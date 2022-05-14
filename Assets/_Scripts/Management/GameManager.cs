using UnityEngine;
using Aircraft.Control;

namespace Aircraft.Managers
{
    [RequireComponent(typeof(UIManager),typeof(LevelManager))]
    public class GameManager : Singleton<GameManager>
    {
        public GameState CurrentState { get; private set; }

        public int Score { get; private set; }
        public int CurrentCheckpointIndex { get; private set; }

        public bool AllObjectivesComplete { get; private set; }
        public Transform CurrentTargetTransform { get; private set; }

        public Player Player { get; private set; }
        public GameObject PlayerObject
        {
            get { return playerObject; }
            set { playerObject = value; }
        }
        [SerializeField] private GameObject playerObject;

        [SerializeField] private int maxOutRangeDelay = 10;

        private int _outRangeDelayCounter;

        private UIManager _uiManager;
        private LevelManager _levelManager;

        protected override void Awake()
        {
            base.Awake();
            Player = PlayerObject.GetComponent<Player>();
            _uiManager = GetComponent<UIManager>();
            _levelManager = GetComponent<LevelManager>();
        }
        private void Start()
        {
            CurrentCheckpointIndex = 0;
            _outRangeDelayCounter = maxOutRangeDelay;
            ChangeState(GameState.GameStarted);
        }

        #region Game States
        public void ChangeState(GameState newState)
        {
            if (CurrentState == newState) return;

            CurrentState = newState;
            switch (newState)
            {
                case GameState.GameAwaitingStart:
                    GameAwaitingStartState();
                    break;
                case GameState.GameStarted:
                    GameStartedState();
                    break;
                case GameState.GameWon:
                    GameWonState();
                    break;
                case GameState.GameLost:
                    GameLostState();
                    break;
                default:
                    throw new System.ArgumentException("Invalid game state selection.");
            }
        }
        private void GameAwaitingStartState()
        {
            //throw new System.NotImplementedException();
        }
        private void GameStartedState()
        {
            //throw new System.NotImplementedException();
        }
        private void GameWonState()
        {
            _uiManager.GameWon();
        }
        private void GameLostState()
        {
            _uiManager.GameLost();
        }
        #endregion

        #region Score Related
        public void UpdateScore(int score)
        {
            Score += score;
            _uiManager.UpdateScore();
        }
        #endregion

        #region Mission - Objective - Checkpoint Related
        public void HitCheckpoint(int score)
        {
            CurrentCheckpointIndex++;
            _levelManager.NextCheckpoint();
            UpdateScore(score);
        }
        public void AssignCurrentTarget(Transform target)
        {
            CurrentTargetTransform = target;
        }
        public void AllObjectivesAreCompleted()
        {
            AllObjectivesComplete = true;
            _uiManager.AllObjectivesCompleted();
        }

        public void OutsideRange()
        {
            _outRangeDelayCounter--;
            _uiManager.OutsideRangeWarning();
            if (_outRangeDelayCounter < 0)
            {
                ChangeState(GameState.GameLost);
            }
        }
        public void InsideRange()
        {
            _outRangeDelayCounter = maxOutRangeDelay;
            _uiManager.DismissOutsideRangeWarning();
        }
        #endregion
    }
    public enum GameState
    {
        GamePreStart = 0,
        GameAwaitingStart = 1,
        GameStarted = 2,
        GameWon = 3,
        GameLost = 4,
    }
}