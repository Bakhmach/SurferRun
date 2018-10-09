using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SurferRun.Util;

namespace SurferRun
{
    public class Clouds : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private int cloudCount;
        [SerializeField] private MinMax scaleRange;
        [SerializeField] private float startHeight;
        [SerializeField] private MinMax heightOffset;
        [SerializeField] private float xOffset;

        [SerializeField] private Image cloudPrefab;
        [SerializeField] private Sprite[] cloudSprites;

        private List<Image> clouds;
        private RectTransform rect;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        void Start()
        {
            rect = GetComponent<RectTransform>();

            clouds = new List<Image>(cloudCount);
            for (int i = 0; i < cloudCount; ++i)
            {
                var cloud = Instantiate(cloudPrefab, rect);
                ChangeCloud(cloud);
                SetRandomPosition(cloud.transform);
                clouds.Add(cloud);
            }
        }

        void Update()
        {
            foreach (var cloud in clouds)
            {
                cloud.transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);

                if (cloud.transform.localPosition.x < rect.rect.xMin - xOffset)
                {
                    SetStartPosition(cloud.transform);
                    ChangeCloud(cloud);
                }
            }
        }

        void ChangeCloud(Image cloud)
        {
            cloud.sprite = cloudSprites[Random.Range(0, cloudSprites.Length)];

            float scale = Random.Range(scaleRange.min, scaleRange.max);
            cloud.transform.localScale = new Vector3(scale, scale);
        }

        void SetRandomPosition(Transform obj)
        {
            float xPos = Random.Range(rect.rect.xMin - xOffset, rect.rect.xMax + xOffset);
            float yPos = startHeight + Random.Range(heightOffset.min, heightOffset.max);
            obj.localPosition = new Vector3(xPos, yPos);
        }

        void SetStartPosition(Transform obj)
        {
            float xPos = rect.rect.xMax + xOffset;
            float yPos = startHeight + Random.Range(heightOffset.min, heightOffset.max);
            obj.localPosition = new Vector3(xPos, yPos);
        }
    }
}
