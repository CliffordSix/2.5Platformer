using UnityEngine;
using System.Collections;
using System;

public class Wander : Behaviour {

    public float walkSpeed = 1.0f;
    int dir = 1;

	override protected void Update () {

        body.AddForce(new Vector2(walkSpeed * dir, 0.0f), ForceMode2D.Impulse);
        System.Random dirChange = new System.Random();
        if(dirChange.Next(0, 100) > 90)
        {
            dir *= -1;
        }

    }
}
