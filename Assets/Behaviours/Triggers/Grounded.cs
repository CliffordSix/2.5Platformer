﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Behaviours.Triggers
{
    public class Grounded : Trigger
    {
        public LayerMask groundLayers;
        new public Collider2D collider;
        new public Rigidbody2D rigidbody;

        bool grounded = false;
        bool lastGrounded = false;

        protected override bool CheckActive()
        {
            return grounded && lastGrounded;
        }

        void FixedUpdate()
        {
            lastGrounded = grounded;
            grounded = collider.IsTouchingLayers(groundLayers) && rigidbody.velocity.y <= 0;
        }
    }
}
