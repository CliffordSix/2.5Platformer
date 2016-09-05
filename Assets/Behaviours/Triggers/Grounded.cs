using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Behaviours.Triggers
{
    public class Grounded : BehaviourTrigger
    {
        public LayerMask groundLayers;
        new public Collider2D collider;

        bool grounded = false;
        bool lastGrounded = false;

        public override bool IsActive()
        {
            return grounded && lastGrounded;
        }

        void FixedUpdate()
        {
            lastGrounded = grounded;
            grounded = collider.IsTouchingLayers(groundLayers);
        }
    }
}
