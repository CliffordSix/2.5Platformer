using UnityEngine;
using System.Collections;

public class Charge : Behaviour {

    public float chargeSpeed = 10.0f;
    int direction = 1;

    void OnEnable()
    {
        ChangeDirection();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeDirection();
    }

    void ChangeDirection()
    {
        if (manager.target.position.x < body.position.x)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }

    protected override void Update()
    {
        base.Update();
       // Debug.Log("Called");
        
        body.AddForce(new Vector2(chargeSpeed * direction * Time.deltaTime, 0.0f));
    }
}
