using UnityEngine;

namespace Aircraft.Managers
{
    [RequireComponent(typeof(UIManager))]
    public class GameManager : Singleton<GameManager>
    {
        public GameState CurrentState { get; private set; }


        public GameObject Player
        {
            get { return player; }
            set { player = value; }
        }
        [SerializeField] private GameObject player;

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
                case GameState.GameCheckingResults:
                    GameCheckingResultsState();
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
            throw new System.NotImplementedException();
        }
        private void GameStartedState()
        {
            throw new System.NotImplementedException();
        }
        private void GameCheckingResultsState()
        {
            throw new System.NotImplementedException();
        }
        private void GameWonState()
        {
            throw new System.NotImplementedException();
        }
        private void GameLostState()
        {
            throw new System.NotImplementedException();
        }
    }
    public enum GameState
    {
        GamePreStart = 0,
        GameAwaitingStart = 1,
        GameStarted = 2,
        GameCheckingResults = 3,
        GameWon = 4,
        GameLost = 5,
    }
}