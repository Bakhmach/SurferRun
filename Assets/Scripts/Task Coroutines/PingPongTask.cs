using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun.TaskCoroutine
{
    public class PingPongTask : BaseTask
    {
        private System.Action<float> action;
        float deltaMove, delay;

        public PingPongTask(System.Action<float> action, float dMove, float delay, bool startNow = true)
        {
            this.action = action;
            this.deltaMove = dMove;
            this.delay = delay;

            if (startNow) Start();
        }

        protected override IEnumerator TaskImplement()
        {
            float factor = 0.0f;

            while(factor < 1.0f)
            {
                action(factor);
                factor += deltaMove;
                yield return null;
            }

            yield return new WaitForSeconds(delay);

            while (factor > 0.0f)
            {
                action(factor);
                factor -= deltaMove;
                yield return null;
            }

        }
    }
}
