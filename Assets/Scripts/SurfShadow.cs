using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurferRun.TaskCoroutine;

namespace SurferRun
{
    public class SurfShadow : MonoBehaviour
    {

        [SerializeField] private Vector3 jumpScale;
        private Vector3 deltaPos;
        private Vector3 startScale;
        private bool canMove = true;

        private PingPongTask scaleTask;
        private Surfer surfer;

        void Start()
        {

            surfer = GameHandler.Instance.surfer;
            deltaPos = transform.position - surfer.transform.position;
            startScale = transform.localScale;

            scaleTask = new PingPongTask((t) =>
            {
                transform.localScale = Vector3.Lerp(startScale, jumpScale, t);
            }, surfer.TimeMove, surfer.TimeFreeze, false);

            surfer.OnStateChanged += SurferStateChanged;
        }

        private void SurferStateChanged(Surfer.State state)
        {
            if (state == Surfer.State.Up)
            {
                canMove = false;
                scaleTask.Start();
            }

            if (state == Surfer.State.Idle && !canMove)
            {
                canMove = true;
            }
        }

        void Update()
        {
            if (canMove)
                transform.position = surfer.transform.position + deltaPos;
        }
    }
}
