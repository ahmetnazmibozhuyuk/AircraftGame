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
            if (GameManager.instance.PlayerObject == null) return;

            transform.SetPositionAndRotation(
                Vector3.Lerp(transform.position,GameManager.instance.PlayerObject.transform.position,Time.deltaTime*cameraPositionDelay), 
                Quaternion.Lerp(transform.rotation, GameManager.instance.PlayerObject.transform.rotation, Time.deltaTime*cameraRotationDelay)
                );
        }
    }
}
