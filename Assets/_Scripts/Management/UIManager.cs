using UnityEngine;
using TMPro;

namespace Aircraft.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI gameStateText;

        [SerializeField] private GameObject outsideRangeWarning;
        [SerializeField] private GameObject tryAgainButton;
        [SerializeField] private GameObject objectiveArrow;
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject stateTextBackground;

        [SerializeField] private TMP_Dropdown aircraftSelectMenu;
        [SerializeField] private TMP_Dropdown courseSelectMenu;

        public void UpdateScore()
        {
            scoreText.SetText("Score: " + GameManager.instance.Score.ToString());
        }
        public void SelectAndStart() // This method is calld from the Start button.
        {
            GameManager.instance.InitializeGame(courseSelectMenu.value, aircraftSelectMenu.value);
        }
        public void GameStarted()
        {
            aircraftSelectMenu.gameObject.SetActive(false);
            courseSelectMenu.gameObject.SetActive(false);
            startButton.SetActive(false);
        }
        public void AllObjectivesCompleted()
        {
            objectiveArrow.SetActive(true);
            gameStateText.SetText("Land the plane!");
            gameStateText.gameObject.SetActive(true);
            stateTextBackground.SetActive(true);
        }
        public void GameWon()
        {
            gameStateText.SetText("YOU WON!");
            gameStateText.gameObject.SetActive(true);
            tryAgainButton.SetActive(true);
            stateTextBackground.SetActive(true);
        }
        public void GameLost()
        {
            gameStateText.SetText("YOU LOST!");
            gameStateText.gameObject.SetActive(true);
            tryAgainButton.SetActive(true);
            stateTextBackground.SetActive(true);
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
