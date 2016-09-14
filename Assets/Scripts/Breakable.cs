using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {

    public Trigger trigger;
    public Animator animator;
    public float animationOffset = 0.0f;

    bool lastTrigger = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(trigger.IsActive() && !lastTrigger)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            Vector3 pos = transform.position;
            pos.y += animationOffset;
            transform.position = pos;
            animator.Play("Broken");
        }

        lastTrigger = trigger.IsActive();
	}
}
