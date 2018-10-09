using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SurferRun.UI
{
    public class LivesUI : MonoBehaviour
    {

        [SerializeField] private Image liveIconPrefab;
        [SerializeField] private Transform livesContainer;

        private LivesController livesController;
        private Image[] liveIcons;

        void Start()
        {
            livesController = GameHandler.Instance.lives;

            liveIcons = new Image[livesController.MaxLives];
            for (int i = 0; i < livesController.MaxLives; ++i)
            {
                liveIcons[i] = Instantiate(liveIconPrefab, livesContainer);
            }

            livesController.OnChanged += LivesChanged;
        }

        private void LivesChanged(int lives)
        {
            for (int i = 0; i < liveIcons.Length; ++i)
            {
                liveIcons[i].enabled = i < lives;
            }
        }
    }
}
