using UnityEngine;
using System.Collections;
using System;

namespace Behaviours.Triggers
{
    [RequireComponent(typeof(Collider2D))]
    public class Collider : Trigger
    {
        public string requiredTag;
        public LayerMask layerMask;

        bool triggered = false;

        public override bool IsActive()
        {
            return triggered;
        }

        void Check(GameObject o)
        {
            if (requiredTag != null && requiredTag.Length > 0 && o.tag != requiredTag)
                return;

            int result = layerMask.value & 1 << o.layer;
            if (layerMask.value == 0 || result > 0)
            {
                triggered = true;
            }
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            Check(collision.gameObject);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            Check(other.gameObject);
        }
    }
}