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

        [System.ComponentModel.DefaultValue(null)]
        public Collider2D other { get; set; }

        bool triggered = false;

        protected override bool CheckActive()
        {
            return triggered;
        }

        bool Check(GameObject o)
        {
            if (requiredTag != null && requiredTag.Length > 0 && o.tag != requiredTag)
                return false;

            int result = layerMask.value & 1 << o.layer;
            return layerMask.value == 0 || result > 0;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            
            if (triggered) return;
            bool result = Check(collision.gameObject);
            if(result)
            {
                triggered = true;
                other = collision.collider;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider != other) return;
            bool result = Check(collision.gameObject);
            if(result)
            {
                triggered = false;
                other = null;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.name == "Eyeball")
                Debug.Log("Collision");
            if (triggered) return;
            bool result = Check(other.gameObject);
            if (result)
            {
                triggered = true;
                this.other = other;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other != this.other) return;
            bool result = Check(other.gameObject);
            if (result)
            {
                triggered = false;
                other = null;
            }
        }
    }
}