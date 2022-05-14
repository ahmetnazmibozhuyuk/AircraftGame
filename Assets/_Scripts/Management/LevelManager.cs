using UnityEngine;
using UnityEngine.SceneManagement;

namespace Aircraft.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform checkpointParent;
        [SerializeField] private Transform landingTransform;

        [SerializeField] private float maxDistanceFromTarget;

        private GameObject[] _checkpoint;

        private string _currentScene;

        private float _distanceFromObjective;

        private float _elapsedTime;
        private readonly float _timeInterval = 2;

        private void Start()
        {
            InitializeCheckpoints();
        }
        private void Update()
        {
            CheckObjectiveDistance(); // Distance operation is expensive so this method is called every other second.
        }
        private void CheckObjectiveDistance()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime < _timeInterval) return;

            _elapsedTime = 0;
            _distanceFromObjective = Vector3.Distance(GameManager.instance.PlayerObject.transform.position, GameManager.instance.CurrentTargetTransform.position);
            if(_distanceFromObjective > maxDistanceFromTarget)
            {
                GameManager.instance.OutsideRange();
            }
            else
            {
                GameManager.instance.InsideRange();
            }
        }

        public void NextCheckpoint()
        {
            _checkpoint[GameManager.instance.CurrentCheckpointIndex-1].SetActive(false);

            if (GameManager.instance.CurrentCheckpointIndex < _checkpoint.Length)
            {
                _checkpoint[GameManager.instance.CurrentCheckpointIndex].SetActive(true);
                GameManager.instance.AssignCurrentTarget(_checkpoint[GameManager.instance.CurrentCheckpointIndex].transform);
            }
            else
            {
                GameManager.instance.AllObjectivesAreCompleted();
                GameManager.instance.AssignCurrentTarget(landingTransform);
            }
        }
        private void InitializeCheckpoints()
        {
            _checkpoint = new GameObject[checkpointParent.childCount];
            for (int i = 0; i < _checkpoint.Length; i++)
            {
                _checkpoint[i] = checkpointParent.GetChild(i).gameObject;
                _checkpoint[i].SetActive(false);
            }
            _currentScene = SceneManager.GetActiveScene().name;
            _checkpoint[0].SetActive(true);
            GameManager.instance.AssignCurrentTarget(_checkpoint[0].transform);
        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(_currentScene);
        }


    }
}
