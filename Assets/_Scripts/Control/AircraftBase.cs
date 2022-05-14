using UnityEngine;
using Aircraft.Managers;

namespace Aircraft.Control
{
    public abstract class AircraftBase : MonoBehaviour
    {
        private bool _hasLeftStartingPosition = false;

        private bool _isInSafeZone = true;

        private float _lastYPosition;

        private float _yDisplacement;

        private float _elapsedTime;

        private readonly float _timeInterval = 1;

        private readonly float _maxYDisplacementForLanding = 20;


        protected virtual void Update()
        {
            CalculateYSpeed();
        }
        private void CalculateYSpeed() // Y displacement per second to check if the aircraft lands smoothly.
        {
            if (GameManager.instance.CurrentState != GameState.GameStarted) return;
            _elapsedTime += Time.deltaTime;
            _yDisplacement += (transform.position.y - _lastYPosition);
            _lastYPosition = transform.position.y;
            if (_elapsedTime > _timeInterval)
            {
                Debug.Log(_yDisplacement);
                _yDisplacement = 0;
                _elapsedTime = 0;
            }
        }

        public void IsItInSafeZone(bool inZone)
        {
            _isInSafeZone = inZone;
        }
        public void LeftStartingPosition()
        {
            _hasLeftStartingPosition = true;
        }
        #region Landing - Crash
        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (GameManager.instance.CurrentState != GameState.GameStarted) return;

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
                if (_yDisplacement < -_maxYDisplacementForLanding)
                {
                    CrashLanding();
                }
                else
                {
                    PerfectLanding();
                }
            }
        }
        protected virtual void CrashLanding()
        {
            GameManager.instance.UpdateScore(10);
            GameManager.instance.ChangeState(GameState.GameWon);
            Debug.Log("CRASH LANDING!");
        }
        protected virtual void PerfectLanding()
        {
            GameManager.instance.UpdateScore(50);
            GameManager.instance.ChangeState(GameState.GameWon);
            Debug.Log("PERFECT LANDING!");
        }
        protected virtual void Crash()
        {
            GameManager.instance.ChangeState(GameState.GameLost);
        }
        #endregion
    }
}
