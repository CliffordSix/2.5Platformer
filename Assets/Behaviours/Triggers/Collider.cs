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

        public Triggers.Timer timer;

        [System.ComponentModel.DefaultValue(null)]
        public Collider2D other { get; set; }

        bool triggered = false;
        public bool toggleMode;

        protected override bool CheckActive()
        {
            return triggered;
        }

        void Update()
        {
            if(timer != null && timer.IsActive())
            {
                triggered = false;
                other = null;
            }
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
            
            if (triggered && !toggleMode) return;
            bool result = Check(collision.gameObject);
            if(result)
            {
                if (toggleMode)
                {
                    triggered = !triggered;
                }
                else
                {
                    triggered = true;
                }
                other = collision.collider;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (toggleMode)
                return;
           
            if (other == null)
            {
                triggered = false;
                this.other = null;
            }
            if (collision.collider != other) return;
            bool result = Check(collision.gameObject);
            if(result)
            {
                triggered = false;
                this.other = null;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (triggered && !toggleMode) return;
            bool result = Check(other.gameObject);
            if (result)
            {
                if (toggleMode)
                {
                    triggered = !triggered;
                }
                else
                {
                    triggered = true;
                }
                this.other = other;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {

            if (toggleMode)
                return;

            if (other == null)
            {
                triggered = false;
                this.other = null;
            }
            if (other != this.other) return;
            bool result = Check(other.gameObject);
            if (result)
            {
                triggered = false;
                this.other = null;
            }
        }
    }
}