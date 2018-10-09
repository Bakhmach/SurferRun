using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun.TaskCoroutine
{
    public class TaskManager : MonoBehaviour
    {
        private static TaskManager instance;

        public static TaskManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var obj = new GameObject("Task Manager");
                    instance = obj.AddComponent<TaskManager>();
                }

                return instance;
            }
        }

        public Coroutine AddTask(BaseTask task)
        {
            return task != null ? StartCoroutine(task.Task()) : null;
        }

        public void StopTask(Coroutine taskCoroutine)
        {
            if (taskCoroutine != null) StopCoroutine(taskCoroutine);
        }

    }
}
