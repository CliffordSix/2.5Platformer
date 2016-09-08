using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class RoomTracker : MonoBehaviour {

    public Room current { get; set; }
    string roomTag = "Room";

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == roomTag)
            current = other.GetComponent<Room>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == roomTag && current == other.GetComponent<Room>())
            other = null;
    }
}
