using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Aircraft.Managers;

namespace Aircraft.Control
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
    public class Controller : MonoBehaviour
    {
        [SerializeField] private float acceleratiionMultiplier;

        [SerializeField] private float maxSpeed;

        [SerializeField] private Slider accelerator;

        private Rigidbody _rigidbody;
        private PlayerInput _input;

        private Vector3 _rotateVector;
        private Vector3 _forwardSpeed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            AssignPositionRotation();
        }
        private void FixedUpdate()
        {
            SetControl();
        }

        private void SetControl()
        {
            _forwardSpeed = accelerator.value * acceleratiionMultiplier * Time.deltaTime * transform.forward;
            _rotateVector = _input.actions["Rotate"].ReadValue<Vector2>();
        }

        private void AssignPositionRotation()
        {
            _rigidbody.AddRelativeTorque(new Vector3(-_rotateVector.y, _rotateVector.x, _rotateVector.x));
            _rigidbody.MovePosition(transform.position + _forwardSpeed);
            //_rigidbody.AddRelativeForce(_forwardSpeed);
        }
    }
}
