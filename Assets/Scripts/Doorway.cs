using UnityEngine;
using System.Collections;

public class Doorway : MonoBehaviour {

    public GameObject rContainer;
    public string dir;
    public bool Connected = false;

    void Start()
    {
        if(!rContainer.GetComponent<RoomManager>())
        {
            Debug.Log("A Doorway is not linked to the Room Manager problems will occour when running the game. Check all room prefabs");
        }
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

	public void buildRoom()
	{
		
		if (Connected == false) {
			string doorToUse = "Null";
			int rNumber = -1;
			bool BuildRoom = false;
			int idx = 0;
		//	Debug.Log ("Building a Room");
			do {
				if (idx >= 50) {
					Debug.Log ("After 50 searches we can't find a room to fit this door check that a room with the correct facing door exists");
					break;
				}

                //Create a new Room
                rNumber = Random.Range(0, rContainer.GetComponent<RoomManager>().RoomList.Length);
                
				GameObject toBuild = rContainer.GetComponent<RoomManager> ().RoomList [rNumber];
				switch (dir) {
				case "N":
					if (toBuild.GetComponent<Room> ().Down) {
                            if (CheckSpace(transform.position + new Vector3(0, toBuild.GetComponent<Room>().Height, 0))
                                && CheckSpace(transform.position + new Vector3(toBuild.GetComponent<Room>().Width,1,0)))
                            {
                                if (Physics2D.Raycast(transform.position + Vector3.up, Vector2.up, toBuild.GetComponent<Room>().Height))
                                {
                                    Debug.Log("Not Enough Room for this room here");
                                    break;
                                }
                                else
                                {
                                    BuildRoom = true;
                                    doorToUse = "S";
                                }
                            }
					}

					break;
				case "E":
					if (toBuild.GetComponent<Room> ().Left) {
                            if (CheckSpace(transform.position + new Vector3(1, toBuild.GetComponent<Room>().Height, 0))
                               && CheckSpace(transform.position + new Vector3(toBuild.GetComponent<Room>().Width, 0, 0)))
                            {
                                if (Physics2D.Raycast(transform.position + Vector3.right, Vector2.right, toBuild.GetComponent<Room>().Width))
                                {
                                    Debug.Log("Not Enough Room for this room here");
                                    break;
                                }
                                else
                                {
                                    BuildRoom = true;
                                    doorToUse = "W";
                                }
                            }
					}

					break;
				case "S":
					if (toBuild.GetComponent<Room> ().Up) {
                            if (CheckSpace(transform.position - new Vector3(0, toBuild.GetComponent<Room>().Height, 0))
                               && CheckSpace(transform.position + new Vector3(toBuild.GetComponent<Room>().Width, -1, 0)))
                            {
                                if (Physics2D.Raycast(transform.position + Vector3.down, Vector2.down, toBuild.GetComponent<Room>().Height))
                                {
                                    Debug.Log("Not Enough Room for this room here");
                                    break;
                                }
                                else
                                {
                                    BuildRoom = true;
                                    doorToUse = "N";
                                }
                            }
					}

					break;
				case "W":
					if (toBuild.GetComponent<Room> ().Right) {
                            if (CheckSpace(transform.position + new Vector3(-1, toBuild.GetComponent<Room>().Height, 0))
                               && CheckSpace(transform.position - new Vector3(toBuild.GetComponent<Room>().Width, 0, 0)))
                            {
                                if (Physics2D.Raycast(transform.position + Vector3.left, Vector2.left, toBuild.GetComponent<Room>().Width))
                                {
                                    Debug.Log("Not Enough Room for this room here");
                                    break;
                                }
                                else
                                {
                                    BuildRoom = true;
                                    doorToUse = "E";
                                }
                            }
					}

					break;
				}
				idx++;

			} while (BuildRoom == false);

			if (BuildRoom) {
				Connected = true;
				//Now we build the room
				GameObject toBuild = Instantiate (rContainer.GetComponent<RoomManager> ().RoomList [rNumber]);
				Doorway[] toBuildExits = toBuild.GetComponent<Room> ().GetExits ();
				Vector3 position = transform.localPosition;
				Doorway nDoor = null;
				foreach (Doorway d in toBuildExits) {
					d.rContainer = rContainer;
					if (d.dir == doorToUse) {
						d.Connected = true;
						nDoor = d;
						//break;


					}
				}
				position -= nDoor.transform.localPosition;
				Vector3 parentPos = transform.parent.transform.position;
				position += parentPos;
				toBuild.transform.position = position;
                toBuild.GetComponent<BoxCollider2D>().enabled = false;
				if(toBuild.GetComponent <Room> ().checkCollision ())
                {
                    toBuild.GetComponent<BoxCollider2D>().enabled = true;
                    if(rContainer.GetComponent<RoomManager>().count < 60)
                    {                                         
                        rContainer.GetComponent<RoomManager>().count++;
						float m = (1.0f / 61.0f);
					//	Debug.Log (m);
						GameObject.Find("GUI Manager").BroadcastMessage ("LoadBarInc", m);
                     //   Debug.Log("Adding " + toBuild.name + " to the List");
                        rContainer.GetComponent<RoomManager>().ExpandList.Add(toBuild);
                        
                    }
                }   
            }
		}
	}

    bool CheckSpace(Vector3 Position)
    {

        //Look to see if there is an object at a position
        float radius = 1.0f;
        if(Physics.CheckSphere(Position, radius))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void BlockDoorway()
    {
        GameObject Clone;
      //  Clone.name = "Delete Me";
        switch (dir)
        {

            case "N":
                Clone = Instantiate(rContainer.GetComponent<RoomManager>().Floor, transform.position, Quaternion.Euler(90, 0, 0)) as GameObject;
                Clone.transform.parent = transform;
                break;
            case "E":
                Clone = Instantiate(rContainer.GetComponent<RoomManager>().Wall, transform.position, Quaternion.Euler(90, 90, 0)) as GameObject;
                Clone.transform.parent = transform;
                break;
            case "S":
                Clone = Instantiate(rContainer.GetComponent<RoomManager>().Floor, transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;
                Clone.transform.parent = transform;
                break;
            case "W":
                Clone = Instantiate(rContainer.GetComponent<RoomManager>().Wall, transform.position, Quaternion.Euler(90, -90, 0)) as GameObject;
                Clone.transform.parent = transform;
                break;

        }

        GetComponent<BoxCollider2D>().isTrigger = false;
       
        
    }
}
