using UnityEngine;
using Aircraft.Managers;

namespace Aircraft
{
    public class CheckpointMiss : MonoBehaviour
    {
        private readonly float _rotationSpeed = 60;
        private void Update()
        {
            transform.Rotate(-Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.instance.MissCheckpoint();
            }
        }
    }
}
