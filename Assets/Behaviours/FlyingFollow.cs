using UnityEngine;
using System.Collections;

namespace Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FlyingFollow : MonoBehaviour
    {
        public Trigger trigger;
        public Transform target;
        public float moveForce = 0.0f;
        public float maxSpeed = 0.0f;

        new Rigidbody2D rigidbody;

        // Use this for initialization
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            if (target == null)
                target = PlayerController.it.transform;
        }

        void FixedUpdate()
        {
            if(trigger == null || trigger.IsActive())
            {
                Vector2 position = transform.position;
                Vector2 otherPosition = target.position;
                Vector2 direction = (otherPosition - position).normalized;

                float zRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, zRot);
                Vector3 scale = transform.localScale;
                if (zRot < -90 && zRot > -270)
                    scale.y = -1;
                else
                    scale.y = 1;
                transform.localScale = scale;

                rigidbody.AddForce(direction * moveForce);
                if (rigidbody.velocity.magnitude > maxSpeed)
                    rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
            }
        }
    }
}
