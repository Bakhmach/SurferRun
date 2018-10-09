using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun
{
    public class ScoreController
    {
        public event System.Action<int> OnChanged = delegate { };

        private int score;

        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                OnChanged(score);
            }
        }
    }
}
