using UnityEngine;
using UnityEngine.UI;
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

        public AircraftBase Player { get; private set; }
        public Controller PlayerController { get; private set; }
        public GameObject PlayerObject { get; private set; }

        public bool LandedPerfectly { get; private set; }

        public Slider AcceleratorSlider
        {
            get { return acceleratorSlider; }
            set { acceleratorSlider = value; }
        }
        [SerializeField] private Slider acceleratorSlider;

        public GameObject[] aircraftList;
        public Transform[] courseList;


        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int maxOutRangeDelay = 10;

        [SerializeField] private int perfectLandingBonusScore;
        [SerializeField] private int crashLandingBonusScore;

        private int _outRangeDelayCounter;

        private UIManager _uiManager;
        private LevelManager _levelManager;

        protected override void Awake()
        {
            base.Awake();

            _uiManager = GetComponent<UIManager>();
            _levelManager = GetComponent<LevelManager>();
        }

        private void SelectAircraft(int aircraftIndex)
        {
            PlayerObject = Instantiate(aircraftList[aircraftIndex], spawnPoint.position, spawnPoint.rotation);
            Player = PlayerObject.GetComponent<AircraftBase>();
            PlayerController = PlayerObject.GetComponent<Controller>();
        }

        private void SelectCheckpointCourse(int courseIndex)
        {
            _levelManager.InitializeCheckpoints(courseList[courseIndex]);
        }
        public void InitializeGame(int courseIndex, int aircraftIndex)
        {
            SelectCheckpointCourse(courseIndex);
            SelectAircraft(aircraftIndex);
            ChangeState(GameState.GameStarted);
        }
        private void Start()
        {
            LandedPerfectly = false;
            CurrentCheckpointIndex = 0;
            _outRangeDelayCounter = maxOutRangeDelay;
            ChangeState(GameState.GameAwaitingStart);

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
            PlayerController.EnableControl();
            _uiManager.GameStarted();
        }
        private void GameWonState()
        {
            _uiManager.GameWon();
            PlayerController.DisableControl();
        }
        private void GameLostState()
        {
            _uiManager.GameLost();
            PlayerController.DisableControl();
        }
        #endregion

        #region Score Related
        public void UpdateScore(int score)
        {
            Score += score;
            _uiManager.UpdateScore();
        }
        public void PerfectLanding()
        {
            LandedPerfectly = true;
            UpdateScore(perfectLandingBonusScore);
        }
        public void CrashLanding()
        {
            UpdateScore(crashLandingBonusScore);
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