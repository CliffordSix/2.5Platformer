using UnityEngine;
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
        List<GameObject> rooms = new List<GameObject>(RoomManager.it.RoomList).FindAll(room => 
            //use this to filter which rooms can be chosen 
            room != parent.prefab
        );

        //Try 5 different rooms, if none fit, leave the door blank
        for (int i = 0; i < 5; i++)
        {
            int r = Random.Range(0, rooms.Count);
            Room prefab = rooms[r].GetComponent<Room>();
            prefab.FindDoors();
            List<Doorway> doors = prefab.GetExits();

            Vector2 bottomLeft = parent.transform.position;
            
            Doorway oppositeDoor = prefab.GetExit(oppositeDoors[dir]);
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
                    bottomLeft.y -= prefab.Height;
                    bottomLeft.x += xDiff;
                    break;
                case "W":
                    bottomLeft.x -= prefab.Width;
                    bottomLeft.y += yDiff;
                    break;
            }

            Vector2 topRight = bottomLeft + new Vector2(prefab.Width, prefab.Height);
            //If the overlap hits something, try a new room
            Collider2D hit = Physics2D.OverlapArea(bottomLeft + Vector2.one, topRight - Vector2.one, LayerMask.GetMask(new string[] { "Room" }));
            if (hit != null)
                continue;

            Room toBuild = Instantiate(prefab.gameObject).GetComponent<Room>();
            toBuild.prefab = prefab.gameObject;
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
