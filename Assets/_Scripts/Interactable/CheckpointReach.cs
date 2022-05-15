using UnityEngine;
using Aircraft.Managers;

namespace Aircraft.Interactable
{
    //Both successful and unsuccessful checkpoint interactions are called from the same class with different point and particle effects.
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
                GameObject particle = Instantiate(hitParticle, transform.position, transform.rotation);
                Destroy(particle, 1);
            }
        }
    }
}
