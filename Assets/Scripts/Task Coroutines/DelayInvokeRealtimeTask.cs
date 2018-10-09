using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun.TaskCoroutine
{
    public sealed class DelayInvokeRealtimeTask : BaseTask
    {
        private float time;
        private System.Action action;

        public DelayInvokeRealtimeTask(float time, System.Action action, bool startNow = true)
        {
            this.time = time;
            this.action = action;

            if (startNow) Start();
        }

        protected override IEnumerator TaskImplement()
        {
            yield return new WaitForSecondsRealtime(time);
            action();
        }
    }
}
