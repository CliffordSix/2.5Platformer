using UnityEngine;
using System.Collections;
using System;

/****************************
 * 							*
 * 	    The Ultimate 		*
 *	    Idle Script        	*
 * 							*
 ****************************/

public class Wander : Behaviour {

    public float walkSpeed = 1.0f;
    int dir = 1;

   // public MeshRenderer text;

    bool dChange = true;
    bool CheckAbove = true;
    public LayerMask Plat;

    override protected void Update() {
       // Debug.DrawRay(transform.position + Vector3.up, Vector3.up * 2.0f, Color.blue);
       // text.transform.localScale = new Vector3(dir * 0.05f,0.05f,0.05f);
        if(CheckAbove == true)
        {
            body.AddForce(new Vector2(walkSpeed * dir, 0.0f), ForceMode2D.Force);
            if (Physics2D.Raycast(transform.position + Vector3.up, Vector3.up, 2.0f, Plat))
            {
                CheckAbove = false;
                StartCoroutine("JumpToPlat");
            }
        }

     /*   if(UnityEngine.Random.Range(0,10) > 5)
        {
            //text.enabled = true;
            StartCoroutine("tText");
        }*/

        gameObject.transform.localScale = new Vector3(dir, 1, 1);
       
        if(dChange)
        {
            StartCoroutine("DirChange");
        }
    }


    /*IEnumerator tText()
    {
        yield return new WaitForSeconds(1.0f);
        text.enabled = false;
    } */

    void OnCollisionEnter2D(Collision2D col)
    { 
        if(col.gameObject.tag != "Player" && col.gameObject.layer != 8 && col.gameObject.layer != 9)
        {
            dir *= -1;
        }
    }

    IEnumerator DirChange()
    {
        dChange = false;
        yield return new WaitForSeconds(5.0F);
        System.Random dirChange = new System.Random();
        if (dirChange.Next(0, 100) > 90)
        {
            dir *= -1;
        }
        dChange = true;
    }

    IEnumerator JumpToPlat()
    {
        //Jump up to the platform above
        if(UnityEngine.Random.Range(0, 3) > 1)
            body.AddForce(new Vector2(10 * dir, 0.0f), ForceMode2D.Impulse);

        body.velocity = new Vector2(body.velocity.x / 3, 0);

        body.AddForce(new Vector2(0.0f, 8.0f), ForceMode2D.Impulse);
        Physics2D.IgnoreLayerCollision(14, 9, true);
        StartCoroutine(turnOnCollision(0.8f));
        yield return new WaitForSeconds(2.0F);
        CheckAbove = true;
    }

    IEnumerator turnOnCollision(float time)
    {
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreLayerCollision(14, 9, false);
    }


    bool IsGrounded()
    {
        Vector2 size = body.GetComponent<BoxCollider2D>().size;
        Vector2 tLeft = body.position + new Vector2(size.x * -0.5f, size.y * 0.5f);
        Vector2 bRight = body.position + new Vector2(size.x * 0.5f, size.y * -0.5f);

        return Physics2D.OverlapArea(tLeft, bRight, 8);
    }
}
