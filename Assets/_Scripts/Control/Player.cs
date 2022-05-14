using UnityEngine;
using Aircraft.Managers;

namespace Aircraft.Control
{
    [RequireComponent(typeof(Controller))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject explosionParticle;

        [SerializeField] private ParticleSystem leftEngineParticle;
        [SerializeField] private ParticleSystem rightEngineParticle;

        private ParticleSystem.MainModule _leftEngine;
        private ParticleSystem.MainModule _rightEngine;

        private Controller _controller;

        private bool _hasLeftStartingPosition = false;

        private bool _isInSafeZone = true;
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

        #region Crash - Hitting Ground
        public void IsItInSafeZone(bool inZone)
        {
            _isInSafeZone = inZone;
        }
        public void LeftStartingPosition()
        {
            _hasLeftStartingPosition = true;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (!_isInSafeZone)
            {
                Crash();
                return;
            }
            if (_isInSafeZone && _hasLeftStartingPosition && !GameManager.instance.AllObjectivesComplete)
            {
                Crash();
                return;
            }

            if (_isInSafeZone && GameManager.instance.AllObjectivesComplete)
            {
                if (_controller.AcceleratorVal > 0.4f)
                {
                    CrashLanding();
                }
                else
                {
                    PerfectLanding();
                }
            }

        }
        private void CrashLanding()
        {
            _controller.RemoveDrag();
            GameManager.instance.UpdateScore(10);
            GameManager.instance.ChangeState(GameState.GameWon);
        }
        private void PerfectLanding()
        {
            _controller.RemoveDrag();
            GameManager.instance.UpdateScore(50);
            GameManager.instance.ChangeState(GameState.GameWon);
        }
        private void Crash()
        {
            _controller.RemoveDrag();
            GameManager.instance.ChangeState(GameState.GameLost);
            explosionParticle.SetActive(true);
        }
        #endregion

    }
}
