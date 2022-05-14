﻿using UnityEngine;
using UnityEngine.InputSystem;
using Aircraft.Managers;

namespace Aircraft.Control
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInput), typeof(AircraftBase))]
    public class Controller : MonoBehaviour
    {
        public float AcceleratorVal { get; private set; }

        [SerializeField] private AircraftIdentity _aircraftIdentity;

        private Rigidbody _rigidbody;
        private PlayerInput _input;

        private Vector3 _rotateVector;
        private Vector3 _forwardSpeed;

        private float _throttle;

        private bool _controlsEnabled;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();
        }
        private void Update()
        {
            if (!_controlsEnabled) return;
            AssignForwardMovement();
            AssignRotation();
        }
        private void FixedUpdate()
        {
            if (!_controlsEnabled) return;
            SetControl();
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
            GameManager.instance.AcceleratorSlider.value += _input.actions["Acceleration"].ReadValue<Vector2>().y*0.03f;
            AcceleratorVal = GameManager.instance.AcceleratorSlider.value;
            _rigidbody.drag = AcceleratorVal * 2;

            _throttle = Mathf.Lerp(_throttle, AcceleratorVal, Time.deltaTime * _aircraftIdentity.accelerationMultiplier);

            _forwardSpeed = _throttle * _aircraftIdentity.maxSpeed * Time.deltaTime * transform.forward;
            _rotateVector = _input.actions["Rotate"].ReadValue<Vector2>();
        }
        private void AssignRotation()
        {
            _rigidbody.AddRelativeTorque(
                new Vector3(-_rotateVector.y* _aircraftIdentity.xTorqueMultiplier*_throttle,
                _rotateVector.x* _aircraftIdentity.yTorqueMultiplier* _throttle,
                -_rotateVector.x* _aircraftIdentity.zTorqueMultiplier* _throttle)); // Rotation based on input

            if (_rotateVector == Vector3.zero)                         // Readjusting rotation when there are no input
            {
                _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation,
                    Quaternion.LookRotation(transform.forward, Vector3.up), 100 * Time.deltaTime * _aircraftIdentity.noInputReadjustSpeed));
            }
        }
        private void AssignForwardMovement()
        {
            _rigidbody.MovePosition(transform.position + _forwardSpeed);
        }
        #endregion
    }
}
