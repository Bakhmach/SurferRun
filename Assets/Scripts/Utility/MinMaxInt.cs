using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurferRun.Util
{
    [System.Serializable]
    public class MinMaxInt
    {
        public int min;
        public int max;

        public MinMaxInt(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
