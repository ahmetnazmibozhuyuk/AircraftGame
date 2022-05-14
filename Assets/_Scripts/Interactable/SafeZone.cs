using UnityEngine;
using Aircraft.Control;
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
            GameManager.instance.Player.LeftStartingPosition();
        }
    }
}
