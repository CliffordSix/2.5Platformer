using UnityEngine;
using System.Collections;

namespace Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public float moveForce = 0.0f;
        public float maxSpeed = 0.0f;

        new Rigidbody2D rigidbody;

        int idleState = Animator.StringToHash("Base.Idle");
        int walkState = Animator.StringToHash("Base.Walk");

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            float input = Input.GetAxis("Horizontal");
            if (input == 0) return;

            if((input < 0 && rigidbody.velocity.x > 0) || (input > 0 && rigidbody.velocity.x < 0))
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            
            rigidbody.AddForce(new Vector2(input * moveForce, 0));
            if (rigidbody.velocity.x > maxSpeed)
                rigidbody.velocity = new Vector2(maxSpeed, rigidbody.velocity.y);
            if(rigidbody.velocity.x < maxSpeed * -1)
                rigidbody.velocity = new Vector2(maxSpeed * -1, rigidbody.velocity.y);
        }

        // Update is called once per frame
        void Update()
        {
            float input = Input.GetAxis("Horizontal");
            Vector3 scale = transform.localScale;
            float facing = input;
            scale.x = facing == 0 ? scale.x : facing < 0 ? -1 : 1;

            ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
            foreach(ParticleSystem p in particles)
            {
                p.transform.localScale = scale;
            }
            transform.localScale = scale;

            Animator animator = GetComponentInChildren<Animator>();
            if (rigidbody.velocity.x < 0.1 && rigidbody.velocity.x > -0.1 && animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                animator.SetTrigger("Stop Walk");
            }
            else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") && (rigidbody.velocity.x >= 0.1 || rigidbody.velocity.x < -0.1))
            {
                animator.SetTrigger("Start Walk");
            }
        }
    }
}
