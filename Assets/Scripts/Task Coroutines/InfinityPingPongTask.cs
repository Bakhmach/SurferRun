using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun.TaskCoroutine
{
    public sealed class InfinityPingPongTask : PingPongTask
    {
        public InfinityPingPongTask(Action<float> action, float dMove, float delay, bool startNow = true)
            : base(action, dMove, delay, startNow) { }

        protected override IEnumerator TaskImplement()
        {
            while(true)
                yield return base.TaskImplement();
        }
    }
}
