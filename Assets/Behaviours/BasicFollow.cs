using UnityEngine;
using System.Collections;

namespace Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BasicFollow : MonoBehaviour
    {

        public Trigger trigger;
        public Transform target;
        public float speed = 0.0f;
        public Collider2D collider;

        new Rigidbody2D rigidbody;
        bool collided = false;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (trigger == null || trigger.IsActive())
            {
                Vector3 position = transform.position;
                int direction = 0;
                if (position.x < target.position.x) direction = 1;
                if (position.x > target.position.x) direction = -1;

                Vector2 velocity = rigidbody.velocity;
                velocity.x = direction * speed;
                rigidbody.velocity = velocity;

                Vector3 scale = transform.localScale;
                scale.x = direction;
                transform.localScale = scale;
            }
        }

        //void OnCollisionEnter2D(Collision2D collision)
        //{
        //    if(collision.transform == target)
        //        collided = true;
        //}

        //void OnCollisionExit2D(Collision2D collision)
        //{
        //    if (collision.transform == target)
        //        collided = false;
        //}
    }
}
