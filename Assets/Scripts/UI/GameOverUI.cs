using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SurferRun.TaskCoroutine;

namespace SurferRun.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Text gameOverText;
        [SerializeField] private float delay = 2.0f;

        void Start()
        {
            GameHandler.Instance.lives.OnChanged += LivesChanged;
        }

        private void LivesChanged(int livesCount)
        {
            if (livesCount == 0)
            {
                Time.timeScale = 0.0f;
                gameOverText.enabled = true;

                new DelayInvokeRealtimeTask(delay, () =>
                {

                    gameOverText.enabled = false;
                    Time.timeScale = 1.0f;

                    GameHandler.Instance.RestartGame();
                });
            }
        }
    }
}
