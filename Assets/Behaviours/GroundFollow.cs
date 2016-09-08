using UnityEngine;
using System.Collections;

namespace Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GroundFollow : MonoBehaviour
    {

        public Trigger trigger;
        public Transform target;
        public float speed = 0.0f;

        new Rigidbody2D rigidbody;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            if (target == null)
                target = PlayerController.it.transform;
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
    }
}
