using System;
using UnityEngine;

namespace NeuroDerby.Core
{
    public class WaitUntilOrSeconds : CustomYieldInstruction
    {
        private float waitedTime;
        private float timeToWait;
        private Func<bool> condition;

        public WaitUntilOrSeconds(Func<bool> condition, float timeToWait)
        {
            waitedTime = 0;
            this.timeToWait = timeToWait;
            this.condition = condition;
        }

        public override bool keepWaiting
        {
            get
            {
                waitedTime += Time.deltaTime;
                return !condition() && waitedTime < timeToWait;
            }
        }
    }
}