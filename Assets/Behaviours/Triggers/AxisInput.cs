using UnityEngine;
using System.Collections;
using System;

namespace Behaviours.Triggers
{
    public class AxisInput : Trigger
    {

        public string axis;

        protected override bool CheckActive()
        {
            return Input.GetAxis(axis) != 0.0f;
        }
    }
}
