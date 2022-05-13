using UnityEngine;
using Aircraft.Managers;

namespace Aircraft.Interactable
{
    public class CheckpointHit : MonoBehaviour
    {
        private readonly float _rotationSpeed = 80;
        private void Update()
        {
            transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.instance.HitCheckpoint();
            }
        }
    }
}
