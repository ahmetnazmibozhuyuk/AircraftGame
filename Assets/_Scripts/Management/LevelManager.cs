﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aircraft.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] checkpoint;

        private void Awake()
        {
            
        }
        private void Start()
        {
            //checkpoint[0].SetActive(true);
        }
        // tüm checkpointlerin olduğu empty grup alınacak; tüm elemanlar disable edilip sadece o anki checkpoint enable edilecek.

        public void NextCheckpoint()
        {
            checkpoint[GameManager.instance.CurrentCheckpoint-1].SetActive(false);

            if (GameManager.instance.CurrentCheckpoint < checkpoint.Length)
            {
                checkpoint[GameManager.instance.CurrentCheckpoint].SetActive(true);
            }
            else
            {
                Debug.Log("Current Checkpoint = "+GameManager.instance.CurrentCheckpoint+", READY TO FINISH THE GAME!"+"Checkpoint Length = "+checkpoint.Length);
                GameManager.instance.AllObjectivesAreCompleted();
            }

        }

    }
}
