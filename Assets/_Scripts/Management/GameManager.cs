using UnityEngine;

namespace Aircraft.Managers
{
    [RequireComponent(typeof(UIManager),typeof(LevelManager))]
    public class GameManager : Singleton<GameManager>
    {
        public GameState CurrentState { get; private set; }

        public int CurrentCheckpointIndex { get; private set; }
        public Transform CurrentTargetTransform { get; private set; }

        public bool AllObjectivesComplete { get; private set; }
        public GameObject Player
        {
            get { return player; }
            set { player = value; }
        }
        [SerializeField] private GameObject player;

        private UIManager _uiManager;
        private LevelManager _levelManager;

        protected override void Awake()
        {
            base.Awake();
            _uiManager = GetComponent<UIManager>();
            _levelManager = GetComponent<LevelManager>();
        }
        private void Start()
        {
            CurrentCheckpointIndex = 0;
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
            //throw new System.NotImplementedException();
            _uiManager.GameWon();
        }
        private void GameLostState()
        {
            //throw new System.NotImplementedException();
            _uiManager.GameLost();
        }
        #endregion
        public void HitCheckpoint()
        {
            CurrentCheckpointIndex++;
            _levelManager.NextCheckpoint();
            _uiManager.NextCheckpoint();
        }
        public void MissCheckpoint()
        {
            CurrentCheckpointIndex++;
            _levelManager.NextCheckpoint();
            _uiManager.NextCheckpoint();
            Debug.Log("checkpoint deadzone missed; deducing points");
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

        }
        public void InsideRange()
        {

        }
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