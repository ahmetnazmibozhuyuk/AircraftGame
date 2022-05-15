using UnityEngine;

namespace Aircraft.Control
{
    // todo : Identical to FighterAircraft class - Differentiate it.
    public class Airliner : AircraftBase
    {
        [SerializeField] private GameObject explosionParticle;

        [SerializeField] private ParticleSystem leftEngineParticle;
        [SerializeField] private ParticleSystem rightEngineParticle;

        private ParticleSystem.MainModule _leftEngine;
        private ParticleSystem.MainModule _rightEngine;

        protected override void Awake()
        {
            base.Awake();
            _leftEngine = leftEngineParticle.main;
            _rightEngine = rightEngineParticle.main;
        }
        private void FixedUpdate()
        {
            AssignEngineParticleValue();
        }

        #region Airplane Particle Effects
        private void AssignEngineParticleValue()
        {
            _leftEngine.startLifetime = _controller.AcceleratorVal;
            _rightEngine.startLifetime = _controller.AcceleratorVal;
        }
        protected override void Crash()
        {
            base.Crash();
            explosionParticle.SetActive(true);
        }
        #endregion
    }
}
