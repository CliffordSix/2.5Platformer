using UnityEngine;
using System.Collections.Generic;


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

    public int Width, Height;

    public SpawnArea[] flyingSpawns;
    public SpawnArea[] groundSpawns;

    public int maxSpawns = 0;

    Dictionary<string, Doorway> doors = new Dictionary<string, Doorway>();

    /*	void OnDrawGizmos()
        {
            Gizmos.DrawLine(tLeft.position, bRight.position);
        }*/

    void Start()
    {
        FindDoors();
    }

    public void FindDoors()
    {
        if (doors.Count > 0) return;
        foreach (Doorway door in GetComponentsInChildren<Doorway>())
        {
            doors.Add(door.dir, door);
        }
    }

    public Doorway GetExit(string direction)
    {
        //Debug.Log(direction);
        return doors[direction];
    }

    public List<Doorway> GetExits()
    {
        Doorway[] doorways = GetComponentsInChildren<Doorway>();
        List<Doorway> result = new List<Doorway>();
        foreach(Doorway door in doorways)
        {
            if (!door.Connected)
                result.Add(door);
        }
        return result;
    }

	bool isOverlapping()
	{
		return Physics2D.OverlapArea (tLeft.position, bRight.position, roomLayer);
	}

	public bool checkCollision()
	{
        //Debug.Log ("Checking for overlapping rooms");
        GetComponent<BoxCollider2D>().enabled = false;
        bool result = !isOverlapping();
        GetComponent<BoxCollider2D>().enabled = true;
        return result;
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
}
