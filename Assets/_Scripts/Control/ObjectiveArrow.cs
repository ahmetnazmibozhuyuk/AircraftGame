using UnityEngine;
using Aircraft.Managers;

namespace Aircraft.Control
{
    public class ObjectiveArrow : MonoBehaviour
    {
        private Vector3 _targetDirection;
        private void Update()
        {
            ArrowPointTowardsTarget();
        }
        private void ArrowPointTowardsTarget()
        {
            _targetDirection = Vector3.RotateTowards(transform.forward,
                GameManager.instance.CurrentTargetTransform.position - transform.position, 1, 0.0f);
            transform.rotation = Quaternion.LookRotation(_targetDirection);
        }
    }
}
