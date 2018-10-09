using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun.Entities
{
    public sealed class Bird : Enemy
    {
        [SerializeField] private float flyAmplitude;
        [SerializeField] private float flyFreq;

        private float startY;

        void Start()
        {
            startY = transform.position.y;
        }

        public override void Update()
        {
            transform.position = new Vector3(transform.position.x,
                                             startY + flyAmplitude * Mathf.Cos(flyFreq * Time.time),
                                             transform.position.z);

            base.Update();
        }
    }
}
