using UnityEngine;

namespace Aircraft.Control
{

    public class FighterAircraft : AircraftBase
    {
        [SerializeField] private GameObject explosionParticle;

        [SerializeField] private ParticleSystem leftEngineParticle;
        [SerializeField] private ParticleSystem rightEngineParticle;

        private ParticleSystem.MainModule _leftEngine;
        private ParticleSystem.MainModule _rightEngine;

        private Controller _controller;

        private void Awake()
        {
            _controller = GetComponent<Controller>();
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
        #endregion

        protected override void Crash()
        {
            base.Crash();
            explosionParticle.SetActive(true);
        }

    }
}
