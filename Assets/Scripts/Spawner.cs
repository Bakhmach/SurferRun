using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurferRun.TaskCoroutine;
using SurferRun.Entities;
using SurferRun.Util;
using SurferRun.Pooling;

namespace SurferRun
{

    [System.Serializable]
    public class EntitySettings
    {
        public MinMax speedRange;
        public float speedGrowDelta;

        private float speed;
        public float Speed
        {
            get { return speed; }
            set { speed = Mathf.Clamp(value, speedRange.min, speedRange.max); }
        }

        public float borderX;
    }

    public class Spawner : MonoBehaviour
    {

        public enum SpawnPosition { Any, Up, Down }

        [SerializeField] private Transform upSpawn, downSpawn, entitiesParent;

        [SerializeField] private List<EntityStackPool> enemyPools;
        [SerializeField] private EntityStackPool coinPool;

        [SerializeField] private EntitySettings settings;
        [SerializeField] private float spawnTime;

        [SerializeField] private MinMaxInt coinSpawnRange;
        private int coinCounter, coinSpawnRate;

        private InfinityDelayInvokeTask spawnTask;

        void Start()
        {
            spawnTask = new InfinityDelayInvokeTask(spawnTime, SpawnWave);
            Restart();
        }

        private void SpawnWave()
        {
            if (coinCounter++ >= coinSpawnRate)
            {
                SpawnEntity(coinPool);
                coinCounter = 0;
                coinSpawnRate = Random.Range(coinSpawnRange.min, coinSpawnRange.max);
            }
            else
            {
                SpawnEntity(enemyPools[(int)(Random.value * enemyPools.Count)]);
            }

            settings.Speed += settings.speedGrowDelta;
        }

        public void SpawnEntity(EntityStackPool pool)
        {
            var entity = pool.Pull();

            SpawnPosition spawnPos = pool.SpawnPosition;
            Vector3 pos = spawnPos == SpawnPosition.Any ? Random.value > 0.5f ? upSpawn.position : downSpawn.position
                                                        : spawnPos == SpawnPosition.Up ? upSpawn.position : downSpawn.position;

            entity.transform.SetParent(entitiesParent);
            entity.Setup(pos, settings);
        }

        public void Restart()
        {
            RemoveAllEntities();

            settings.Speed = settings.speedRange.min;
            coinSpawnRate = Random.Range(coinSpawnRange.min, coinSpawnRange.max);
            coinCounter = 0;
        }

        public void RemoveAllEntities()
        {
            for (int i = 0; i < entitiesParent.childCount; ++i)
            {
                var entity = entitiesParent.GetChild(i).GetComponent<Entity>();
                if (entity) entity.ReturnToPool();
            }
        }
    }
}
