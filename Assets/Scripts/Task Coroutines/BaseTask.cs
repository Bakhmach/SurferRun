using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun.TaskCoroutine
{
    public abstract class BaseTask 
    {
        public event System.EventHandler OnStop = delegate { };
        public bool IsExecute { get; protected set; }

        private Coroutine coroutine;

        public IEnumerator Task()
        {
            yield return TaskImplement();
            IsExecute = false;
            Stop();
        }

        public void Start()
        {
            if (IsExecute) return;
            coroutine = TaskManager.Instance.AddTask(this);
            IsExecute = true;
        }

        public void Stop()
        {
            if (IsExecute) TaskManager.Instance.StopTask(coroutine);

            IsExecute = false;
            OnStop(this, System.EventArgs.Empty);
        }

        protected abstract IEnumerator TaskImplement();       
    }
}
