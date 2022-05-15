using UnityEngine;
using Aircraft.Managers;

namespace Aircraft.Control
{
    public class CamFollow : MonoBehaviour
    {
        [Tooltip("Higher the value is, lower the delay will be.")]
        [SerializeField] private float cameraPositionDelay = 1;
        [Tooltip("Higher the value is, lower the delay will be.")]
        [SerializeField] private float cameraRotationDelay = 1;

        private void FixedUpdate()
        {
            if (GameManager.instance.PlayerObject == null || GameManager.instance.CurrentState != GameState.GameStarted) return;
                transform.SetPositionAndRotation(CamPosition(), CamRotation());
        }
        private Quaternion CamRotation()
        {
            return Quaternion.Lerp(transform.rotation, GameManager.instance.PlayerObject.transform.rotation, Time.deltaTime * cameraRotationDelay);
        }
        private Vector3 CamPosition()
        {
            return Vector3.Lerp(transform.position, GameManager.instance.PlayerObject.transform.position, Time.deltaTime * cameraPositionDelay);
        }
    }
}
