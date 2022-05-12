using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject winText;
        [SerializeField] private GameObject loseText;



        public void NextCheckpoint()
        {

        }
        public void GameWon()
        {
            winText.SetActive(true);
        }
        public void GameLost()
        {
            loseText.SetActive(true);
        }
    }
}
