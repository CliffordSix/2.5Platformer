using UnityEngine;
using System.Collections;


/****************************
 * 							*
 * 							*
 *	Fix the Box Sizing		*
 * 							*
 * 							*
 ****************************/
public class Room : MonoBehaviour {

    public bool Up ,Down,Left ,Right;

    public bool collided;

	public Transform tLeft, bRight;

	public LayerMask roomLayer;

/*	void OnDrawGizmos()
	{
        Gizmos.DrawLine(tLeft.position, bRight.position);
    }*/

    public Doorway[] GetExits()
    {
        Doorway[] doorways = GetComponentsInChildren<Doorway>();
        return doorways;
    }

	bool isOverlapping()
	{
		return Physics2D.OverlapArea (tLeft.position, bRight.position, roomLayer);
	}

    void Start()
	{
        StartCoroutine(CheckForCollision());
    }

	public bool checkCollision()
	{
		//Debug.Log ("Checking for overlapping rooms");
		if (isOverlapping ()) {
			//Debug.Log ("Is Overlapping an existing Room");
			collided = true;

            return false;
		}
        else
        {
            //Renable the collider
            return true;
        }
	}

    /*void OnTriggerEnter2D(Collider2D col)
    {
		if (col.tag == "Player") {
			Debug.Log ("Player Entered Rooms");
			Doorway[] doors = GetExits ();
			foreach (Doorway d in doors) {
				d.buildRoom ();
			}
		}

    }*/

    IEnumerator CheckForCollision()
    {
        yield return new WaitForSeconds(1.5f);

        //Debug.Log(collided);
        if(collided)
        {
          //  Debug.Log("Rrmoving Room");
            Destroy(this.gameObject);
        }

    }

   

}
