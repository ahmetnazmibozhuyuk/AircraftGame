using UnityEngine;
using Aircraft.Managers;

namespace Aircraft.Interactable
{
    public class CheckpointReach : MonoBehaviour
    {
        [SerializeField] private GameObject hitParticle;

        [SerializeField] private int hitScore = 20;

        [SerializeField] private float rotationSpeed = 80;
        private void Update()
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.instance.HitCheckpoint(hitScore);
                var particle = Instantiate(hitParticle, transform.position, transform.rotation);
                Destroy(particle, 1);
            }
        }
    }
}
