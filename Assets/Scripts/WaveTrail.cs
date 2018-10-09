using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurferRun.TaskCoroutine;

namespace SurferRun
{
    public class WaveTrail : MonoBehaviour
    {
        [SerializeField] private float hideTime = 0.2f;

        new private SpriteRenderer renderer;
        private Vector3 deltaPos;
        private Surfer surfer;
        private bool canMove = true;
        private DelayInvokeTask hideTask;

        void Start()
        {

            surfer = GameHandler.Instance.surfer;
            deltaPos = transform.position - surfer.transform.position;
            renderer = GetComponent<SpriteRenderer>();

            hideTask = new DelayInvokeTask(hideTime, () => { renderer.enabled = false; }, false);

            surfer.OnStateChanged += SurferStateChanged;
        }

        private void SurferStateChanged(Surfer.State state)
        {
            if (state == Surfer.State.Up)
            {
                canMove = false;
                hideTask.Start();
            }

            if (state == Surfer.State.Idle && !renderer.enabled)
            {
                canMove = true;
                renderer.enabled = true;
            }
        }

        void Update()
        {
            if (canMove)
                transform.position = surfer.transform.position + deltaPos;
        }
    }
}
