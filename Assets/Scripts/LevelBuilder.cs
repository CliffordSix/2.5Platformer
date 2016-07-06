using UnityEngine;
using System.Collections.Generic;

public class LevelBuilder : MonoBehaviour {

    //How many times the world will generate new rooms in the dungeon
    public int worldSize;

    void Start()
    {
        BuildWorld(); 
    }

    void BuildWorld()
    {
        Queue<GameObject> Rooms = new Queue<GameObject>();
        //Get a Starting room to build from and we can move from there
        int StartingRoom = Random.Range(1, 5);
        GameObject Room = Instantiate(Resources.Load("Prefabs/Room" + StartingRoom), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        //Add the room to the queue 
        Rooms.Enqueue(Room);
        Doorway[] Exits;//= Room.GetComponent<Room>().GetExits();
        Doorway[] nExits;
        for (int i = 0; i < worldSize; i++)
        {
            Debug.Log(i);
            if (Rooms.Count > 0)
            {
                //Pull the first room from the queue and get its exits
                GameObject toUse = Rooms.Dequeue();
                Exits = toUse.GetComponent<Room>().GetExits();
                bool RoomAdded = false;
                //loop through all the doors in the original room
                foreach (Doorway D in Exits)
                {
                    //If the Doorway is not connected then continue
                    if (D.Connected)
                    {
                        Debug.Log("Door is already linked up");
                        continue;
                    }
                    //Get the direction of the exit
                    string ExitDir = D.dir;
                    do
                    {
                        //Pick a random room to try
                        int nRoomInt = Random.Range(1, 5);

                        // GameObject nRoom = Instantiate(Resources.Load("Prefabs/Room" + nRoomInt), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                        GameObject newRoom = Resources.Load("Prefabs/Room" + nRoomInt) as GameObject;
                        Room r = newRoom.GetComponent<Room>();
                        nExits = newRoom.GetComponent<Room>().GetExits();

                        //Loop through all the exits in the new room room
                        foreach (Doorway E in nExits)
                        {
                            if (E.Connected)
                            {
                                Debug.Log("Door is already linked up");
                                break;
                            }
                            switch (ExitDir)
                            {
                                case "N":
                                    if (E.dir == "S")
                                    {
                                        //Move the room
                                        GameObject nRoom = Instantiate(newRoom, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                                        Debug.Log("Joining a North Exit to a South Entrance");
                                        Debug.Log("Room Number: " + Room.name + "To Room Number: " + nRoom.name);
                                        Rooms.Enqueue(linkRooms(toUse, nRoom, E, D));
                                        RoomAdded = true;
                                    }
                                    break;
                                case "S":
                                    if (E.dir == "N")
                                    {
                                        //Move the room
                                        GameObject nRoom = Instantiate(newRoom, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                                        Debug.Log("Joining a South Exit to a North Entrance");
                                        Debug.Log("Room Number: " + Room.name + "To Room Number: " + nRoom.name);
                                        Rooms.Enqueue(linkRooms(toUse, nRoom, E, D));
                                        RoomAdded = true;

                                    }
                                    break;
                                case "E":
                                    if (E.dir == "W")
                                    {
                                        //Move the room
                                        GameObject nRoom = Instantiate(newRoom, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                                        Debug.Log("Joining an East Exit to a West Entrance");
                                        Debug.Log("Room Number: " + Room.name + "To Room Number: " + nRoom.name);
                                        RoomAdded = true;
                                        Rooms.Enqueue(linkRooms(toUse, nRoom, E, D));
                                    }
                                    break;
                                case "W":
                                    if (E.dir == "E")
                                    {
                                        //Move the room
                                        GameObject nRoom = Instantiate(newRoom, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                                        Debug.Log("Joining a West Exit to a East Entrance");
                                        Debug.Log("Room Number: " + Room.name + "To Room Number: " + nRoom.name);
                                        Rooms.Enqueue(linkRooms(toUse, nRoom, E, D));
                                        RoomAdded = true;

                                    }
                                    break;
                            }
                        }
                    } while (RoomAdded == false);
                }
            }
        }
    }

    GameObject linkRooms(GameObject Base, GameObject r, Doorway Entrance, Doorway Exit)
    {
        r.transform.position = Base.transform.position + Exit.transform.localPosition;
        r.transform.position = r.transform.position - Entrance.transform.localPosition;
        Entrance.Connected = true;
        Exit.Connected = true;

        return r;
    }

}
