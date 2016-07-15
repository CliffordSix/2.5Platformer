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

	void OnDrawGizmos()
	{
		Vector3 Center = new Vector3 ((bRight.position.x + tLeft.position.x) / 2, (tLeft.position.y + bRight.position.y) / 2, -1);
		Gizmos.color = new Color (Random.Range(0,1), 1, 0, 0.25f);
		Vector3 Size = new Vector3 (bRight.position.x - Center.x, tLeft.position.y - Center.y, 0.25f);
		Gizmos.DrawCube(Center, Center);
	}

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

	public void checkCollision()
	{
		Debug.Log ("Checking for overlapping rooms");
		if (isOverlapping ()) {
			Debug.Log ("Is Overlapping an existing Room");
			collided = true;
		}
	}

    void OnTriggerEnter2D(Collider2D col)
    {
		if (col.tag == "Player") {
			Debug.Log ("Player Entered Rooms");
			Doorway[] doors = GetExits ();
			foreach (Doorway d in doors) {
				d.buildRoom ();
			}
		}

    }

    IEnumerator CheckForCollision()
    {
        yield return new WaitForSeconds(1.5f);

        //Debug.Log(collided);
        if(collided)
        {
            Debug.Log("Rrmoving Room");
            Destroy(this.gameObject);
        }

    }

   

}
