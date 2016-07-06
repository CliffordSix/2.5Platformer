using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DungeonBuilder : MonoBehaviour
{
    public List<Doorway> OpenDoors = new List<Doorway>();
    public List<Doorway> ClosedDoors = new List<Doorway>();
    //The number of rooms that there are to try
    int RoomCount = 5;
    int RoomsToExpand = 4;
    void Start()
    {
        //Pick a starting room
        int StartingRoom = Random.Range(1, 5);
        GameObject Room = Instantiate(Resources.Load("Prefabs/Room" + StartingRoom), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        //Create the list of OpenDoors
        OpenDoors = Room.GetComponent<Room>().GetExits().ToList();
        //We then loop across all these doors and add a room to them
        do
        {
            int nRoomInt = Random.Range(1, 5);
            GameObject newRoom = Resources.Load("Prefabs/Room" + nRoomInt) as GameObject;

            switch (OpenDoors[0].dir)
            {
                case "N":
                    if (newRoom.GetComponent<Room>().Down)
                    {
                        //These rooms are linkable
                        foreach (Doorway Entry in newRoom.GetComponent<Room>().GetExits())
                        {
                            if (Entry.dir == "S")
                            {
                                newRoom = Instantiate(newRoom, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                                newRoom.transform.position = OpenDoors[0].transform.parent.position + OpenDoors[0].transform.localPosition;
                                newRoom.transform.position = newRoom.transform.position - Entry.transform.localPosition;
                                ClosedDoors.Add(Entry);
                                OpenDoors.RemoveAt(0);
                            }
                            else
                            {
                                if (0 < RoomsToExpand)
                                    OpenDoors.Add(Entry);
                            }
                        }
                    }
                    break;
                case "S":
                    if (newRoom.GetComponent<Room>().Up)
                    {
                        //These rooms are linkable
                        foreach (Doorway Entry in newRoom.GetComponent<Room>().GetExits())
                        {
                            if (Entry.dir == "N")
                            {
                                newRoom = Instantiate(newRoom, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                                newRoom.transform.position = OpenDoors[0].transform.parent.position + OpenDoors[0].transform.localPosition;
                                newRoom.transform.position = newRoom.transform.position - Entry.transform.localPosition;
                                ClosedDoors.Add(Entry);
                                OpenDoors.RemoveAt(0);
                            }
                            else
                            {
                                if (0 < RoomsToExpand)
                                    OpenDoors.Add(Entry);
                            }
                        }
                    }
                    break;
                case "E":
                    if (newRoom.GetComponent<Room>().Left)
                    {
                        //These rooms are linkable
                        foreach (Doorway Entry in newRoom.GetComponent<Room>().GetExits())
                        {
                            if (Entry.dir == "W")
                            {
                                newRoom = Instantiate(newRoom, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                                newRoom.transform.position = OpenDoors[0].transform.parent.position + OpenDoors[0].transform.localPosition;
                                newRoom.transform.position = newRoom.transform.position - Entry.transform.localPosition;
                                ClosedDoors.Add(Entry);
                                OpenDoors.RemoveAt(0);
                            }
                            else
                            {
                                if (0 < RoomsToExpand)
                                    OpenDoors.Add(Entry);
                            }
                        }
                    }
                    break;
                case "W":
                    if (newRoom.GetComponent<Room>().Right)
                    {
                        //These rooms are linkable
                        foreach (Doorway Entry in newRoom.GetComponent<Room>().GetExits())
                        {
                            if (Entry.dir == "E")
                            {
                                newRoom = Instantiate(newRoom, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                                newRoom.transform.position = OpenDoors[0].transform.parent.position + OpenDoors[0].transform.localPosition;
                                newRoom.transform.position = newRoom.transform.position - Entry.transform.localPosition;
                                ClosedDoors.Add(Entry);
                                OpenDoors.RemoveAt(0);
                            }
                            else
                            {
                                if (0 < RoomsToExpand)
                                    OpenDoors.Add(Entry);
                            }
                        }
                    }
                    break;
            }
        } while (OpenDoors.Count > 0);

    }
}

