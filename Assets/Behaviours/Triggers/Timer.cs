using UnityEngine;
using System.Collections;
using System;

namespace Behaviours.Triggers
{
    public class Timer : Trigger
    {
        public float interval = 0.0f;
        public Trigger resetTrigger;

        float time = 0.0f;
        bool running = true;
        bool triggered = false;

        protected override bool CheckActive()
        {
            return triggered;
        }

        public void Reset()
        {
            time = 0.0f;
            triggered = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (triggered) triggered = false;

            if(running)
            {
                time += Time.deltaTime;
                if(time > interval)
                {
                    triggered = true;
                    time = 0.0f;
                }
            }

            if(resetTrigger != null && resetTrigger.IsActive())
            {
                Reset();
            }
        }
    }
}
