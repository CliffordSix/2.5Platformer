using UnityEngine;
using System.Collections;

namespace Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GroundFollow : MonoBehaviour
    {

        public Trigger trigger;
        public Transform target;
        public float moveForce = 0.0f;
        public float maxSpeed = 0.0f;

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
                Vector2 direction = Vector2.zero;
                if (position.x < target.position.x) direction.x = 1;
                if (position.x > target.position.x) direction.x = -1;

                rigidbody.AddForce(direction * moveForce);

                Vector3 scale = transform.localScale;
                scale.x = direction.x;
                transform.localScale = scale;

                if (rigidbody.velocity.magnitude > maxSpeed)
                    rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
            }
        }
    }
}
