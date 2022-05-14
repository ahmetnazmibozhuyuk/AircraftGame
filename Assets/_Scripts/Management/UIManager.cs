using UnityEngine;
using TMPro;

namespace Aircraft.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameStateTextObj;
        [SerializeField] private TextMeshProUGUI gameStateText;

        [SerializeField] private GameObject outsideRangeWarning;
        [SerializeField] private GameObject tryAgainButton;
        [SerializeField] private GameObject objectiveArrow;

        [SerializeField] private TextMeshProUGUI scoreText;

        public void UpdateScore()
        {
            scoreText.SetText("Score: "+GameManager.instance.Score.ToString());
        }
        public void GameWon()
        {
            gameStateText.SetText("YOU WON!");
            gameStateTextObj.SetActive(true);
        }
        public void GameLost()
        {
            gameStateText.SetText("YOU LOST!");
            gameStateTextObj.SetActive(true);
            tryAgainButton.SetActive(true);
        }
        public void AllObjectivesCompleted()
        {
            objectiveArrow.SetActive(true);
            gameStateText.SetText("GO TO THE LANDING ZONE!");
            gameStateTextObj.SetActive(true);
        }
        public void OutsideRangeWarning()
        {
            outsideRangeWarning.SetActive(true);
        }
        public void DismissOutsideRangeWarning()
        {
            outsideRangeWarning.SetActive(false);
        }
    }
}
