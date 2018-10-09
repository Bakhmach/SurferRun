using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun
{
    public class SurferController : MonoBehaviour
    {
        [SerializeField] private Surfer surfer;

        void Update()
        {

            float axis = Input.GetAxisRaw("Vertical");

            if (axis > 0.0f) surfer.MoveUp();
            if (axis < 0.0f) surfer.MoveDown();

        }
    }
}
