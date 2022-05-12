using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] checkpoint;



        private int _currentCheckpointIndex = 0;

        private void Awake()
        {
            
        }
        private void Start()
        {
            //checkpoint[0].SetActive(true);
        }

        public void NextCheckpoint()
        {
            checkpoint[_currentCheckpointIndex-1].SetActive(false);

            if (_currentCheckpointIndex > checkpoint.Length - 1)
            {

            }
            checkpoint[_currentCheckpointIndex].SetActive(true);
        }

    }
}
