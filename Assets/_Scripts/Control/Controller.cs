using UnityEngine;
using UnityEngine.InputSystem;
using Aircraft.Managers;

namespace Aircraft.Control
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
    public class Controller : MonoBehaviour
    {
        public float AcceleratorVal { get; private set; }

        [SerializeField] private AircraftIdentity _aircraftIdentity;

        private Rigidbody _rigidbody;
        private PlayerInput _input;

        private Vector3 _rotateVector;
        private Vector3 _forwardSpeed;

        private readonly float _leftStickSensitivity = 0.03f;
        private float _throttle;

        private bool _controlsEnabled;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();
        }
        private void FixedUpdate()
        {

            AssignForwardMovement();
            if (!_controlsEnabled)
            {
                SafeLandingSetControl();
                return;
            }
            SetControl();
            AssignRotation();

        }

        public void EnableControl()
        {
            _controlsEnabled = true;
            _rigidbody.drag = 0;
            _rigidbody.angularDrag = 10;
        }
        public void DisableControl()
        {
            _controlsEnabled = false;
            _rigidbody.drag = 0;
            _rigidbody.angularDrag = 0;
        }

        #region Control and Movement
        private void SetControl()
        {
            GameManager.instance.AcceleratorSlider.value += _input.actions["Acceleration"].ReadValue<Vector2>().y * _leftStickSensitivity;
            AcceleratorVal = GameManager.instance.AcceleratorSlider.value;
            _rotateVector = _input.actions["Rotate"].ReadValue<Vector2>();

            _rigidbody.drag = AcceleratorVal * 2;
            _throttle = Mathf.Lerp(_throttle, AcceleratorVal,  _aircraftIdentity.accelerationMultiplier);
            _forwardSpeed = _throttle * _aircraftIdentity.maxSpeed *  transform.forward;
        }
        private void SafeLandingSetControl()
        {
            if (GameManager.instance.LandedPerfectly)
            {
                if (_throttle > 0) _throttle -= 0.007f;
                else _throttle = 0;
                _forwardSpeed = _throttle * _aircraftIdentity.maxSpeed *  transform.forward;
                return;
            }
            _throttle = 0;
            _forwardSpeed = _throttle * _aircraftIdentity.maxSpeed *  transform.forward;
        }
        private void AssignRotation()
        {
            _rigidbody.AddRelativeTorque(
                new Vector3(-_rotateVector.y * _aircraftIdentity.xTorqueMultiplier * _throttle,
                _rotateVector.x * _aircraftIdentity.yTorqueMultiplier * _throttle,
                -_rotateVector.x * _aircraftIdentity.zTorqueMultiplier * _throttle)); // Rotation based on input

            if (_rotateVector == Vector3.zero)                         // Readjusting rotation when there are no input
            {
                _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation,
                    Quaternion.LookRotation(transform.forward, Vector3.up),  _aircraftIdentity.noInputReadjustSpeed));
            }
        }
        private void AssignForwardMovement()
        {
            _rigidbody.MovePosition(transform.position + _forwardSpeed);
        }
        #endregion
    }
}
