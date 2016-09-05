﻿using UnityEngine;
using System.Collections;
using System;

namespace Behaviours.Triggers
{
    public class AxisInput : BehaviourTrigger
    {

        public string axis;

        public override bool IsActive()
        {
            return Input.GetAxis(axis) != 0.0f;
        }
    }
}
