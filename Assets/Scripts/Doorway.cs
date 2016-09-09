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
    public bool Connected = false;

    public int DungeonSize = 60;

    void Start()
    {
    }

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

	public GameObject buildRoom()
	{
        //First check there isnt a room directly in front of this doorway
        if (Connected)
            return null;

        Room parent = transform.parent.GetComponent<Room>();
        GameObject[] rooms = RoomManager.it.RoomList;

        for(int i = 0; i < 5; i++)
        {
            int r = Random.Range(0, rooms.Length);
            Room toBuild = rooms[r].GetComponent<Room>();
            toBuild.FindDoors();
            List<Doorway> doors = toBuild.GetExits();

            Vector2 bottomLeft = parent.transform.position;

            Debug.Log(oppositeDoors[dir]);
            Doorway oppositeDoor = toBuild.GetExit(oppositeDoors[dir]);
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
            {
                Selection.activeGameObject = hit.gameObject;
                continue;
            }

            Connected = true;
            oppositeDoor.Connected = true;

            toBuild = Instantiate(toBuild.gameObject).GetComponent<Room>();
            toBuild.transform.position = bottomLeft;

            return toBuild.gameObject;
        }

        return null;
        
        //switch (dir)
        //{ 
            
        //    case "N":
        //        if (!CheckSpace(transform.position + new Vector3(0, 1, 1)))
        //            Connected = true;
        //        break;
        //    case "E":
        //        if(!CheckSpace(transform.position + new Vector3(1, 0, 1)))
        //            Connected = true;
        //        break;
        //    case "W":
        //        if(!CheckSpace(transform.position + new Vector3(-1, 0, 1)))
        //         Connected = true;
        //        break;
        //    case "S":
        //        if(!CheckSpace(transform.position + new Vector3(0, -1, 1)))
        //            Connected = true;
        //        break;
        //}

//        if (Connected == false ) {
//			string doorToUse = "Null";
//			int rNumber = -1;
//			bool BuildRoom = false;
//			int idx = 0;
//		//	Debug.Log ("Building a Room");
//			do {
//				if (idx >= 50) {
////					Debug.Log ("After 50 searches we can't find a room to fit this door check that a room with the correct facing door exists");
//					break;
//				}
//				//Create a new Room
//				rNumber = Random.Range (0, rContainer.GetComponent<RoomManager>().RoomList.Length);      
//				GameObject toBuild = rContainer.GetComponent<RoomManager> ().RoomList [rNumber];
//                switch (dir)
//                {
//                    case "N":
//                        if (toBuild.GetComponent<Room>().Down)
//                        {
//                            if (CheckSpace(transform.position + new Vector3(0, toBuild.GetComponent<Room>().Height, 1))
//                                && CheckSpace(transform.position + new Vector3(toBuild.GetComponent<Room>().Width, 1, 1)))
//                            {
//                                Debug.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + new Vector3(0, toBuild.GetComponent<Room>().Height,0));
//                                if (Physics2D.Raycast(transform.position + Vector3.up, Vector2.up, toBuild.GetComponent<Room>().Height)
//                                    || Physics2D.Raycast(transform.position + Vector3.up, Vector2.right, toBuild.GetComponent<Room>().Width)
//                                    || Physics2D.Raycast(transform.position + Vector3.up, Vector2.left, toBuild.GetComponent<Room>().Width))
//                                {
//                                   // Debug.Log("Not Enough Room for this room here");
//                                    break;
//                                }
//                                else
//                                {
//                                    BuildRoom = true;
//                                    doorToUse = "S";
//                                }
//                            }
//                        }

//                        break;
//                    case "E":
//                        if (toBuild.GetComponent<Room>().Left)
//                        {
//                            if (CheckSpace(transform.position + new Vector3(1, toBuild.GetComponent<Room>().Height, 1))
//                               && CheckSpace(transform.position + new Vector3(toBuild.GetComponent<Room>().Width, 0, 1)))
//                            {
//                                if (Physics2D.Raycast(transform.position + Vector3.right, Vector2.right, toBuild.GetComponent<Room>().Width)
//                                    || Physics2D.Raycast(transform.position + Vector3.right, Vector2.up, toBuild.GetComponent<Room>().Height - 2)
//                                    || Physics2D.Raycast(transform.position + Vector3.right, Vector2.down, toBuild.GetComponent<Room>().Height - 2))
//                                {
////                                    Debug.Log("Not Enough Room for this room here");
//                                    break;
//                                }
//                                else
//                                {
//                                    BuildRoom = true;
//                                    doorToUse = "W";
//                                }
//                            }
//                        }

//                        break;
//                    case "S":
//                        if (toBuild.GetComponent<Room>().Up)
//                        {
//                            if (CheckSpace(transform.position - new Vector3(0, toBuild.GetComponent<Room>().Height, 1))
//                               && CheckSpace(transform.position + new Vector3(toBuild.GetComponent<Room>().Width, -1, 1)))
//                            {
//                                if (Physics2D.Raycast(transform.position + Vector3.down, Vector2.down, toBuild.GetComponent<Room>().Height)
//                                     || Physics2D.Raycast(transform.position - Vector3.up, Vector2.right, toBuild.GetComponent<Room>().Width)
//                                    || Physics2D.Raycast(transform.position - Vector3.up, Vector2.left, toBuild.GetComponent<Room>().Width))
//                                {
//                             //       Debug.Log("Not Enough Room for this room here");
//                                    break;
//                                }
//                                else
//                                {
//                                    BuildRoom = true;
//                                    doorToUse = "N";
//                                }
//                            }
//                        }

//                        break;
//                    case "W":
//                        if (toBuild.GetComponent<Room>().Right)
//                        {
//                            if (CheckSpace(transform.position + new Vector3(-1, toBuild.GetComponent<Room>().Height, 1))
//                               && CheckSpace(transform.position - new Vector3(toBuild.GetComponent<Room>().Width, 0, 1)))
//                            {
//                                if (Physics2D.Raycast(transform.position + Vector3.left, Vector2.left, toBuild.GetComponent<Room>().Width)
//                                    || Physics2D.Raycast(transform.position - Vector3.right, Vector2.up, toBuild.GetComponent<Room>().Height - 2)
//                                    || Physics2D.Raycast(transform.position - Vector3.right, Vector2.down, toBuild.GetComponent<Room>().Height - 2))
//                                {
//                               //     Debug.Log("Not Enough Room for this room here");
//                                    break;
//                                }
//                                else
//                                {
//                                    BuildRoom = true;
//                                    doorToUse = "E";
//                                }
//                            }
//                        }

//                        break;
//                }
//                idx++;

//            } while (BuildRoom == false);


//            if (BuildRoom) {
//				Connected = true;
//				//Now we build the room
//				GameObject toBuild = Instantiate (rContainer.GetComponent<RoomManager> ().RoomList [rNumber]);
//				List<Doorway> toBuildExits = toBuild.GetComponent<Room> ().GetExits ();
//				Vector3 position = transform.localPosition;
//				Doorway nDoor = null;
//				foreach (Doorway d in toBuildExits) {
//					d.rContainer = rContainer;
//					if (d.dir == doorToUse) {
//						d.Connected = true;
//						nDoor = d;
//					}
//				}
//				position -= nDoor.transform.localPosition;
//				Vector3 parentPos = transform.parent.transform.position;
//				position += parentPos;
//				toBuild.transform.position = position;
                
//				if(toBuild.GetComponent <Room> ().checkCollision ())
//                {
//                    return toBuild;
//                }   
//            }
//		}
//        return null;
	}

    public void BlockDoorway()
    {
        GameObject Clone;
      //  Clone.name = "Delete Me";
        switch (dir)
        {

            case "N":
                Clone = Instantiate(RoomManager.it.Floor, transform.position, Quaternion.Euler(90, 0, 0)) as GameObject;
                Clone.transform.parent = transform;
                break;
            case "E":
                Clone = Instantiate(RoomManager.it.Wall, transform.position, Quaternion.Euler(90, 90, 0)) as GameObject;
                Clone.transform.parent = transform;
                break;
            case "S":
                Clone = Instantiate(RoomManager.it.Floor, transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;
                Clone.transform.parent = transform;
                break;
            case "W":
                Clone = Instantiate(RoomManager.it.Wall, transform.position, Quaternion.Euler(90, -90, 0)) as GameObject;
                Clone.transform.parent = transform;
                break;

        }

        GetComponent<BoxCollider2D>().isTrigger = false;
       
        
    }

    bool CheckSpace(Vector3 Position)
    {

        //Look to see if there is an object at a position
        float radius = 0.1f;
        return !Physics2D.OverlapCircle(Position, radius);
    }


}
