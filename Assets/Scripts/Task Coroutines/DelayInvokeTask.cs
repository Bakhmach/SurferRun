using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun.TaskCoroutine
{
    public class DelayInvokeTask : BaseTask
    {
        private float time;
        private System.Action action;

        public DelayInvokeTask(float time, System.Action action, bool startNow = true)
        {
            this.time = time;
            this.action = action;

            if (startNow) Start();
        }

        protected override IEnumerator TaskImplement()
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }
}
