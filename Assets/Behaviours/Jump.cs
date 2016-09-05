using UnityEngine;
using System.Collections;

namespace Behaviours
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class Jump : Behaviour {

        new Rigidbody2D rigidbody;

        public Triggers.Grounded groundedTrigger;
        public float force = 0.0f;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }
        
        void FixedUpdate()
        {
            if(trigger.IsActive() && groundedTrigger.IsActive())
            {
                Debug.Log("ran");
                rigidbody.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            }
        }
    }
}
