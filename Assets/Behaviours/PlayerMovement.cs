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

        void FixedUpdate()
        {
            float input = Input.GetAxis("Horizontal");
            if (input == 0) return;
            Vector2 velocity = rigidbody.velocity;
            velocity.x = input * speed;
            rigidbody.velocity = velocity;
        }

        // Update is called once per frame
        void Update()
        {
            float input = Input.GetAxis("Horizontal");
            Vector3 scale = transform.localScale;
            scale.x = input == 0 ? scale.x : input < 0 ? -1 : 1;

            ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
            foreach(ParticleSystem p in particles)
            {
                p.transform.localScale = scale;
            }
            transform.localScale = scale;
        }
    }
}
