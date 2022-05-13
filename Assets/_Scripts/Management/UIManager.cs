using UnityEngine;


namespace Aircraft.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject winText;
        [SerializeField] private GameObject loseText;
        [SerializeField] private GameObject allObjectivesCompleteText;

        [SerializeField] private GameObject tryAgainButton;

        [SerializeField] private GameObject objectiveArrow;

        private Vector3 _targetDirection;


        private void Update()
        {
            ArrowPointTowardsTarget();
        }
        private void ArrowPointTowardsTarget()
        {
            _targetDirection= Vector3.RotateTowards(objectiveArrow.transform.forward, 
                GameManager.instance.CurrentTargetTransform.position - objectiveArrow.transform.position, 1, 0.0f);
            objectiveArrow.transform.rotation = Quaternion.LookRotation(_targetDirection);
        }

        public void NextCheckpoint()
        {

        }
        public void GameWon()
        {
            allObjectivesCompleteText.SetActive(false);
            winText.SetActive(true);
        }
        public void GameLost()
        {
            allObjectivesCompleteText.SetActive(false);
            loseText.SetActive(true);
            tryAgainButton.SetActive(true);
        }
        public void AllObjectivesCompleted()
        {
            objectiveArrow.SetActive(true);
            allObjectivesCompleteText.SetActive(true);

        }
    }
}
