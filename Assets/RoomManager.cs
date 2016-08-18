using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {

    public int DungeonSize = 60;

    public GameObject[] RoomList = new GameObject[6];
    public int count = 0;
    public GameObject OriginalRoom;

    public GameObject Wall, Floor;

    bool CloseDoors = true;

    public List<GameObject> ExpandList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        Room OR = OriginalRoom.GetComponent<Room>();

        Doorway[] doors = OR.GetExits();
       
        //Shuffle List of doors
        for(int i = 0; i < doors.Length; i++)
        {
            int j = UnityEngine.Random.Range(0, doors.Length);
            Doorway k = doors[i];
            doors[i] = doors[j];
            doors[j] = k; 
           
        }

        foreach (Doorway d in doors)
        {
            d.DungeonSize = DungeonSize;    
            d.buildRoom();      
        }
        count++;
    }
	
	// Update is called once per frame
	void Update () {
	
        if(ExpandList.Count >= 1)
        {
            //Pick a random room to expand
            
            int Rand = UnityEngine.Random.Range(0, ExpandList.Count);

            GameObject roomZero = ExpandList[0];
            ExpandList[0] = ExpandList[Rand];
            ExpandList[Rand] = roomZero;


            Room BuildMe = ExpandList[0].GetComponent<Room>();
            Doorway[] doors = BuildMe.GetExits();
            //Shuffle List of doors
            for (int i = 0; i < doors.Length; i++)
            {
                int j = UnityEngine.Random.Range(0, doors.Length);
                Doorway k = doors[i];
                doors[i] = doors[j];
                doors[j] = k;

            }

            foreach (Doorway d in doors)
            {
              //  Debug.Log(d.dir);
                d.DungeonSize = DungeonSize;
                d.buildRoom();
            }
           // Debug.Log("End");
            ExpandList.RemoveAt(0);
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
				StartCoroutine ("WaitForLoad");
            }
            
            CloseDoors = false;

        }

	}
	IEnumerator WaitForLoad()
	{
		yield return new WaitForSeconds(1.0F);
		GameObject.Find("GUI Manager").BroadcastMessage ("LoadBarInc", 1.0F);
	}
}
