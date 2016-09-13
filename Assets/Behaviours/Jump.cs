using UnityEngine;
using System.Collections;

namespace Behaviours
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class Jump : MonoBehaviour {

        new Rigidbody2D rigidbody;
        
        public Trigger trigger;
        public Triggers.Grounded groundedTrigger;
        public float force = 0.0f;

        bool lastActive = false;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }
        
        void FixedUpdate()
        {
            if((trigger.IsActive() && !lastActive) && groundedTrigger.IsActive())
            {
                rigidbody.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            }
            lastActive = trigger.IsActive();
        }
    }
}
