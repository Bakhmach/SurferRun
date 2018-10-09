using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun.Entities
{
    public class Enemy : Entity
    {
        public bool Scored { get; protected set; }

        public override void Setup(Vector3 pos, EntitySettings settings)
        {
            Scored = false;
            base.Setup(pos, settings);
        }

        public override void Update()
        {
            if (!Scored && transform.position.x < GameHandler.Instance.surfer.transform.position.x)
            {
                GameHandler.Instance.score.Score++;
                Scored = true;
            }

            base.Update();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (!GameHandler.Instance.surfer.Blinking)
                {
                    GameHandler.Instance.lives.Lives--;
                    GameHandler.Instance.surfer.Blinking = true;
                }
            }
        }
    }
}
