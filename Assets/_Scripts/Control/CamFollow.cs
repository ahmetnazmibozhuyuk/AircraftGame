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
            transform.SetPositionAndRotation(
                Vector3.Lerp(transform.position,GameManager.instance.Player.transform.position,Time.deltaTime*cameraPositionDelay), 
                Quaternion.Lerp(transform.rotation, GameManager.instance.Player.transform.rotation, Time.deltaTime*cameraRotationDelay)
                );
        }
    }
}
