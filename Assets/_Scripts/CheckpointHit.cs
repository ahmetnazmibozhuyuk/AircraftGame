using UnityEngine;
using Aircraft.Managers;

namespace Aircraft.Interactable
{
    public class CheckpointHit : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.instance.HitCheckpoint();
                //Destroy(gameObject);
            }
        }
    }
}
