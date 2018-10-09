using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurferRun.TaskCoroutine;

namespace SurferRun
{
    public class GameHandler : MonoBehaviour
    {
        public static GameHandler Instance;

        public Surfer surfer;
        public Spawner spawner;

        public readonly LivesController lives = new LivesController();
        public readonly ScoreController score = new ScoreController();

        void Awake()
        {
            if (Instance == null) Instance = this; else Destroy(gameObject);
        }

        void Start()
        {
            score.Score = 0;
            lives.Restart();
        }

        public void RestartGame()
        {
            spawner.Restart();
            surfer.Restart();

            score.Score = 0;
            lives.Restart();
        }
    }
}