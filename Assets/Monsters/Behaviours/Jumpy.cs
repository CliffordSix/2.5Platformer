using UnityEngine;
using System.Collections;

public class Jumpy : BehaviourOld {

    
    public float jumpForce = 1.0f;
    public float jumpDelay = 0.0f;
    public LayerMask ground;

    float untilJump = 0.0f;

    // Update is called once per frame
    override protected void Update () {
        if (!IsGrounded())
            return;

        if(untilJump <= 0.0f)
        {
            //jump
            untilJump = jumpDelay;
            body.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
        }
        else
        {
            //count down to jump
            untilJump -= Time.deltaTime;
        }
	}

    bool IsGrounded()
    {
        Vector2 size = body.GetComponent<BoxCollider2D>().size;
        Vector2 tLeft = body.position + new Vector2(size.x * -0.5f, size.y * 0.5f);
        Vector2 bRight = body.position + new Vector2(size.x * 0.5f, size.y * -0.5f);

        return Physics2D.OverlapArea(tLeft, bRight, ground);
    }
}
