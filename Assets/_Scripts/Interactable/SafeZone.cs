using UnityEngine;
using Aircraft.Managers;

namespace Aircraft.Interactable
{
    public class SafeZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GameManager.instance.Player.IsItInSafeZone(true);
        }
        private void OnTriggerExit(Collider other)
        {
            GameManager.instance.Player.IsItInSafeZone(false);
            //Once the player leaves the safe zone, they can't re-enter unless all objectives are completed.
            GameManager.instance.Player.LeftStartingPosition();
        }
    }
}
