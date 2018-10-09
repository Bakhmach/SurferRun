using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun.Entities
{
    public sealed class Coin : Entity
    {
        [SerializeField] private int addScore;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GameHandler.Instance.score.Score += addScore;
                ReturnToPool();
            }
        }
    }
}
