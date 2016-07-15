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

   /* void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player" && Connected == false)
        {
            string doorToUse = "Null";
            int rNumber = -1;
            bool BuildRoom = false;
            int idx = 0;
           
            do
            {
                if(idx >= 50)
                {
                    Debug.Log("After 50 searches we can't find a room to fit this door check that a room with the correct facing door exists");
                    break;
                }
                //Create a new Room
                rNumber = Random.Range(0, 3);
                
                GameObject toBuild = rContainer.GetComponent<RoomManager>().RoomList[rNumber];
                switch (dir)
                {
                    case "N":
                        if (toBuild.GetComponent<Room>().Down)
                        {
                            BuildRoom = true;
                            doorToUse = "S";
                        }

                        break;
                    case "E":
                        if (toBuild.GetComponent<Room>().Left)
                        {
                            BuildRoom = true;
                            doorToUse = "W";
                        }

                        break;
                    case "S":
                        if (toBuild.GetComponent<Room>().Up)
                        {
                            BuildRoom = true;
                            doorToUse = "N";
                        }

                        break;
                    case "W":
                        if (toBuild.GetComponent<Room>().Right)
                        {
                            BuildRoom = true;
                            doorToUse = "E";
                        }

                        break;
                }
                idx++;
                    
            } while (BuildRoom == false);

            if(BuildRoom)
            {
                Connected = true;
                //Now we build the room
                GameObject toBuild = Instantiate(rContainer.GetComponent<RoomManager>().RoomList[rNumber]);
                Doorway[] toBuildExits = toBuild.GetComponent<Room>().GetExits();
                Vector3 position = transform.localPosition;
                Doorway nDoor = null;
                foreach(Doorway d in toBuildExits)
                {
                    d.rContainer = rContainer;
                    if(d.dir == doorToUse)
                    {
                        d.Connected = true;
                        nDoor = d;
                        //break;
                        
                        
                    }
                }
                position -= nDoor.transform.localPosition;
                Vector3 parentPos = transform.parent.transform.position;
                position += parentPos;
                toBuild.transform.position = position;
            }

        }
    }*/

	public void buildRoom()
	{
		
		if (Connected == false) {
			string doorToUse = "Null";
			int rNumber = -1;
			bool BuildRoom = false;
			int idx = 0;
			Debug.Log ("Building a Room");
			do {
				if (idx >= 50) {
					Debug.Log ("After 50 searches we can't find a room to fit this door check that a room with the correct facing door exists");
					break;
				}
				//Create a new Room
				rNumber = Random.Range (0, 4);

				GameObject toBuild = rContainer.GetComponent<RoomManager> ().RoomList [rNumber];
				switch (dir) {
				case "N":
					if (toBuild.GetComponent<Room> ().Down) {
						BuildRoom = true;
						doorToUse = "S";
					}

					break;
				case "E":
					if (toBuild.GetComponent<Room> ().Left) {
						BuildRoom = true;
						doorToUse = "W";
					}

					break;
				case "S":
					if (toBuild.GetComponent<Room> ().Up) {
						BuildRoom = true;
						doorToUse = "N";
					}

					break;
				case "W":
					if (toBuild.GetComponent<Room> ().Right) {
						BuildRoom = true;
						doorToUse = "E";
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
				toBuild.GetComponent <Room> ().checkCollision ();
			}
		}
	}

}
