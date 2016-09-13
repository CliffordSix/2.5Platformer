using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Doorway : MonoBehaviour {

    static Dictionary<string, string> oppositeDoors = new Dictionary<string, string>()
    {
        { "N", "S" },
        { "E", "W" },
        { "S", "N" },
        { "W", "E" }
    };

    public GameObject rContainer;
    public string dir;
    public bool Connected { get; set; }

    public int DungeonSize = 60;

    void OnDrawGizmos()
    {
        var scale = 1.0f;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.right * scale);
        Gizmos.DrawLine(transform.position, transform.position + transform.right * scale);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * scale);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.125f);
    }

	public GameObject BuildRoom()
	{
        //First check there isnt a room directly in front of this doorway
        if (Connected)
            return null;

        Room parent = transform.parent.GetComponent<Room>();
        GameObject[] rooms = RoomManager.it.RoomList;

        //Try 5 different rooms, if none fit, leave the door blank
        for(int i = 0; i < 5; i++)
        {
            int r = Random.Range(0, rooms.Length);
            Room toBuild = rooms[r].GetComponent<Room>();
            toBuild.FindDoors();
            List<Doorway> doors = toBuild.GetExits();

            Vector2 bottomLeft = parent.transform.position;
            
            Doorway oppositeDoor = toBuild.GetExit(oppositeDoors[dir]);
            if (oppositeDoor == null)
                continue;
            float xDiff = transform.localPosition.x - oppositeDoor.transform.localPosition.x;
            float yDiff = transform.localPosition.y - oppositeDoor.transform.localPosition.y;

            switch (dir)
            {
                case "N":
                    bottomLeft.y += parent.Height;
                    bottomLeft.x += xDiff;
                    break;
                case "E":
                    bottomLeft.x += parent.Width;
                    bottomLeft.y += yDiff;
                    break;
                case "S":
                    bottomLeft.y -= toBuild.Height;
                    bottomLeft.x += xDiff;
                    break;
                case "W":
                    bottomLeft.x -= toBuild.Width;
                    bottomLeft.y += yDiff;
                    break;
            }

            Vector2 topRight = bottomLeft + new Vector2(toBuild.Width, toBuild.Height);
            //If the overlap hits something, try a new room
            Collider2D hit = Physics2D.OverlapArea(bottomLeft + Vector2.one, topRight - Vector2.one, LayerMask.GetMask(new string[] { "Room" }));
            if (hit != null)
                continue;

            toBuild = Instantiate(toBuild.gameObject).GetComponent<Room>();
            toBuild.FindDoors();

            toBuild.transform.position = bottomLeft;

            oppositeDoor = toBuild.GetExit(oppositeDoors[dir]);
            Connected = true;
            oppositeDoor.Connected = true;

            return toBuild.gameObject;
        }

        return null;
	}

    public void BlockDoorway()
    {
        GameObject Clone = null;
        switch (dir)
        {

            case "N":
                Clone = Instantiate(RoomManager.it.Floor, transform.position, Quaternion.Euler(90, 0, 0)) as GameObject;
                break;
            case "E":
                Clone = Instantiate(RoomManager.it.Wall, transform.position, Quaternion.Euler(90, 90, 0)) as GameObject;
                break;
            case "S":
                Clone = Instantiate(RoomManager.it.Floor, transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;
                break;
            case "W":
                Clone = Instantiate(RoomManager.it.Wall, transform.position, Quaternion.Euler(90, -90, 0)) as GameObject;
                break;

        }
        Clone.transform.parent = transform;
        GetComponent<BoxCollider2D>().isTrigger = false; 
    }
}
