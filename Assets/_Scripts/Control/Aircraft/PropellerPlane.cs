using UnityEngine;

namespace Aircraft.Control
{
    public class PropellerPlane : AircraftBase
    {
        [SerializeField] private GameObject propeller;
        [SerializeField] private GameObject explosionParticle;

        protected override void Update()
        {
            base.Update();
            PropellerMovement();
        }
        private void PropellerMovement()
        {
            propeller.transform.Rotate(Vector3.forward, 2000*_controller.AcceleratorVal * Time.deltaTime);
        }
        protected override void Crash()
        {
            base.Crash();
            explosionParticle.SetActive(true);
        }
    }
}
