using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SurferRun.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private Text scoreText;

        void Start()
        {
            GameHandler.Instance.score.OnChanged += ScoreChanged;
        }

        private void ScoreChanged(int score)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
