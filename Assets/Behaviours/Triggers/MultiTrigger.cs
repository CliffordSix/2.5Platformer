using UnityEngine;
using System.Collections;
using System;

namespace Behaviours.Triggers
{
    public class MultiTrigger : Trigger
    {
        public enum Mode
        {
            AND,
            OR
        }

        public Trigger[] triggers;
        public Mode mode;

        public override bool IsActive()
        {
            foreach(Trigger trigger in triggers)
            {
                if (mode == Mode.AND && !trigger.IsActive())
                    return false;
                if (mode == Mode.OR && trigger.IsActive())
                    return true;
            }
            return false;
        }
    }
}