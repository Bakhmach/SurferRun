using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun
{
    public class LivesController
    {
        public event System.Action<int> OnChanged = delegate { };

        [SerializeField] private int maxLives = 3;

        private int lives;

        public int Lives
        {
            get { return lives; }
            set
            {
                lives = Mathf.Clamp(value, 0, maxLives);
                OnChanged(lives);
            }
        }

        public int MaxLives
        {
            get { return maxLives; }
        }

        public void Restart()
        {
            Lives = maxLives;
        }
    }
}
