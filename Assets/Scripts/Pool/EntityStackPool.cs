using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurferRun.Entities;

namespace SurferRun.Pooling
{
    [System.Serializable]
    public sealed class EntityStackPool : IPool<Entity>
    {
        [SerializeField] private Entity prefab;
        [SerializeField] private Spawner.SpawnPosition spawnPosition;

        private readonly Stack<Entity> stack = new Stack<Entity>();

        public Spawner.SpawnPosition SpawnPosition
        {
            get { return spawnPosition; }
        }

        public int Size
        {
            get { return stack.Count; }
        }

        public Entity Pull()
        {
            if (Size == 0) return CreateElement();

            var element = stack.Pop();
            element.gameObject.SetActive(true);
            return element;
        }

        public void Put(Entity element)
        {
            element.gameObject.SetActive(false);
            stack.Push(element);
        }

        private Entity CreateElement()
        {
            var element = GameObject.Instantiate(prefab);
            element.Pool = this;
            return element;
        }
    }
}
