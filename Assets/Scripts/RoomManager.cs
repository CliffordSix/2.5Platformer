using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {

    public static RoomManager it;

    public int DungeonSize = 60;

    public GameObject[] RoomList = new GameObject[6];
    public GameObject OriginalRoom;

    public GameObject Wall, Floor;

    bool CloseDoors = true;
    int createdCount = 0;

    public List<Doorway> ExpandList = new List<Doorway>();

    void Awake()
    {
        if (it != this)
        {
            it = this;
            Init();
        }
    }

	// Use this for initialization
	void Init () {
        Room OR = OriginalRoom.GetComponent<Room>();

        //Start out with all original doors in list to expand
        ExpandList = OR.GetExits();
       
        //Shuffle List of doors
        //for(int i = 0; i < doors.Length; i++)
        //{
        //    int j = UnityEngine.Random.Range(0, doors.Length);
        //    Doorway k = doors[i];
        //    doors[i] = doors[j];
        //    doors[j] = k;
        //}

        //foreach (Doorway d in doors)
        //{
        //    d.DungeonSize = DungeonSize;    
        //    d.buildRoom();      
        //}
        //count++;
    }

    void Expand(int index)
    {
        //Get the door to be expanded
        Doorway door = ExpandList[index];
        door.DungeonSize = DungeonSize;

        //Remove it so we never check this door again
        ExpandList.RemoveAt(index);
        UnityEditor.Selection.activeGameObject = door.gameObject;

        //Attempt to build a room at the door
        GameObject room = door.buildRoom();

        //If the build failed
        if (room != null)
        {
            createdCount++;
            List<Doorway> doors = room.GetComponent<Room>().GetExits();
            //while (doors.Count > 0)
            //{
            //    int r = Random.Range(0, doors.Count);
            //    ExpandList.Add(doors[r]);
            //    doors.RemoveAt(r);
            //}
            ExpandList.AddRange(doors);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    //If we haven't created all the rooms and there are doors to expand
        if(createdCount < DungeonSize && ExpandList.Count > 0)
        {
            //Expand the first door in the list
            Expand(0);
            GUIManager.it.LoadBarInc(1.0f / (DungeonSize + 1.0f));

            //Room BuildMe = ExpandList[0].GetComponent<Room>();
            //Doorway[] doors = BuildMe.GetExits();
            ////Shuffle List of doors
            //for (int i = 0; i < doors.Length; i++)
            //{
            //    int j = UnityEngine.Random.Range(0, doors.Length);
            //    Doorway k = doors[i];
            //    doors[i] = doors[j];
            //    doors[j] = k;
            //}

            //foreach (Doorway d in doors)
            //{
            //    d.DungeonSize = DungeonSize;
            //    d.buildRoom();
            //}
        }
        else
        {
            if (CloseDoors)
            {
                //Block off all doors
                foreach (GameObject doors in GameObject.FindGameObjectsWithTag("Doorway"))
                {
                    if (doors.GetComponent<Doorway>().Connected == false)
                    {
                        doors.GetComponent<Doorway>().BlockDoorway();
                    }
                }
                Debug.Log("Dungeon Built");
                GameObject.Find("GUI Manager").BroadcastMessage("LoadBarInc", 1.0F);
                
            }
            
            CloseDoors = false;

        }

	}
}
