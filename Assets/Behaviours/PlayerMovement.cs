using UnityEngine;
using System.Collections;

namespace Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public float speed;
        new Rigidbody2D rigidbody;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            float input = Input.GetAxis("Horizontal");
            Vector2 velocity = rigidbody.velocity;
            velocity.x = input * speed;
            rigidbody.velocity = velocity;

            Vector3 scale = transform.localScale;
            scale.x = input < 0 ? -1 : 1;
            transform.localScale = scale;
        }
    }
}
