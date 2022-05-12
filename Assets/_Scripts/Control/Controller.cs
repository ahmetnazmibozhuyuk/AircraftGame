using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aircraft.Managers;

namespace Aircraft.Control
{
    [RequireComponent(typeof(Rigidbody))]
    public class Controller : MonoBehaviour
    {
        [SerializeField] private float turnRate;

        private Rigidbody _rigidbody;

        private Vector3 _hitDownPosition;
        private Vector3 _offset;



        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            AssignRotation();
            transform.localPosition += 10 * Time.deltaTime * transform.forward;

        }
        private void FixedUpdate()
        {
            SetControl();
        }

        private void SetControl()
        {
            _offset = Vector3.zero;
            if (Input.GetMouseButtonDown(0))
            {
                _hitDownPosition = Input.mousePosition;
                _offset = Vector3.zero;
            }
            else if (Input.GetMouseButton(0))
            {
                _offset = Vector3.ClampMagnitude((Input.mousePosition - _hitDownPosition), 20)*0.01f;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _offset = Vector3.zero;


            }
        }




        private void AssignRotation()
        {
            _rigidbody.AddRelativeTorque(new Vector3(-_offset.y,0,-_offset.x),ForceMode.VelocityChange);
        }
    }
}
