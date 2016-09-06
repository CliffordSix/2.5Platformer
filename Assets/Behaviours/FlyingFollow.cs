using UnityEngine;
using System.Collections;

namespace Behaviours
{
    public class FlyingFollow : MonoBehaviour
    {
        public Trigger trigger;
        public Transform target;
        public float speed = 0.0f;

        new Rigidbody2D rigidbody;

        // Use this for initialization
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            if(trigger == null || trigger.IsActive())
            {
                Vector2 position = transform.position;
                Vector2 otherPosition = target.position;
                Vector2 direction = (otherPosition - position).normalized;
                rigidbody.velocity = direction * speed;
                transform.LookAt(target);
            }
        }
    }
}
