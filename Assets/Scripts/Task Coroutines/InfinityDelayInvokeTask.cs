using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun.TaskCoroutine
{

    public sealed class InfinityDelayInvokeTask : DelayInvokeTask
    {
        public InfinityDelayInvokeTask(float time, Action action, bool startNow = true)
            : base(time, action, startNow) { }

        protected override IEnumerator TaskImplement()
        {
            while(true)
                yield return base.TaskImplement();
        }

    }
}
