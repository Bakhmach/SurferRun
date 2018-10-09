using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurferRun.Pooling;

namespace SurferRun.Entities
{
    public abstract class Entity : MonoBehaviour, IPoolable
    {

        public IPool<Entity> Pool { get; set; }

        protected EntitySettings settings;

        public virtual void ReturnToPool()
        {
            Pool.Put(this);
        }

        public virtual void Setup(Vector3 pos, EntitySettings settings)
        {
            transform.position = pos;
            this.settings = settings;
        }

        public virtual void Update()
        {
            transform.Translate(-settings.Speed * Time.deltaTime, 0.0f, 0.0f);

            if (transform.position.x < settings.borderX)
            {
                ReturnToPool();
            }
        }
    }
}
