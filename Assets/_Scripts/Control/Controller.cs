using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Aircraft.Managers;

namespace Aircraft.Control
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
    public class Controller : MonoBehaviour
    {
        public float AcceleratorVal { get; private set; }

        // todo : Scriptable obje haline getirerek uçakları çeşitlendir. Player classını abstract yap böylece çeşitli uçaklar için özelleştirilmiş classlar oluştur.
        #region Aircraft Identity
        [SerializeField] private float maxSpeed;
        [Range(0.01f,0.5f)] [Tooltip("How quickly the plane reaches its max speed.")]
        [SerializeField] private float accelerationMultiplier;
        [Tooltip("Up - down maneuver multiplier.")]
        [SerializeField] private float xTorqueMultiplier = 1;
        [Tooltip("Left - right maneuver multiplier.")]
        [SerializeField] private float yTorqueMultiplier = 1;
        [Tooltip("Roll maneuver multiplier.")]
        [SerializeField] private float zTorqueMultiplier = 1;
        [Tooltip("How quickly the plane readjusts its roll when the input is released.")]
        [SerializeField] private float noInputReadjustSpeed = 1;
        #endregion

        [SerializeField] private Slider accelerator;

        private Rigidbody _rigidbody;
        private PlayerInput _input;

        private Vector3 _rotateVector;
        private Vector3 _forwardSpeed;

        private float _throttle;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();
        }
        private void Update()
        {
            if (GameManager.instance.CurrentState != GameState.GameStarted) return;
            AssignForwardMovement();
            AssignRotation();
        }
        private void FixedUpdate()
        {
            if (GameManager.instance.CurrentState != GameState.GameStarted) return;
            SetControl();
        }
        public void RemoveDrag()
        {
            _rigidbody.drag = 0;
            _rigidbody.angularDrag = 0;
        }
        #region Control and Movement
        private void SetControl()
        {
            AcceleratorVal = accelerator.value;
            _rigidbody.drag = AcceleratorVal * 2;
            _throttle = Mathf.Lerp(_throttle, AcceleratorVal, Time.deltaTime * accelerationMultiplier);

            _forwardSpeed = _throttle * maxSpeed * Time.deltaTime * transform.forward;
            _rotateVector = _input.actions["Rotate"].ReadValue<Vector2>();
        }
        private void AssignRotation()
        {
            _rigidbody.AddRelativeTorque(
                new Vector3(-_rotateVector.y*xTorqueMultiplier, _rotateVector.x*yTorqueMultiplier, _rotateVector.x*zTorqueMultiplier)); // Rotation based on input

            if (_rotateVector == Vector3.zero)                                                              // Readjusting rotation when there are no input
            {
                _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation,
                    Quaternion.LookRotation(transform.forward, Vector3.up), 100 * Time.deltaTime * noInputReadjustSpeed));
            }
        }
        private void AssignForwardMovement()
        {
            _rigidbody.MovePosition(transform.position + _forwardSpeed);
        }
        #endregion
    }
}
