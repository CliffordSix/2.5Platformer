using UnityEngine;
using System.Collections;
using System;

namespace Behaviours.Triggers
{
    public class Timer : Trigger
    {
        public float interval = 0.0f;
        public bool startRunning = true;

        float time = 0.0f;
        bool triggered = false;
        bool running = false;

        void Start()
        {
            running = startRunning;
        }

        protected override bool CheckActive()
        {
            return triggered;
        }

        public void Run()
        {
            running = true;
        }

        public void Pause()
        {
            running = false;
        }

        public void Stop()
        {
            Pause();
            Reset();
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
        }
    }
}
