using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {

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
        foreach (Doorway d in doors)
        {       
            d.buildRoom();      
        }
        count++;
    }
	
	// Update is called once per frame
	void Update () {
	
        if(ExpandList.Count >= 1)
        { 
            Room BuildMe = ExpandList[0].GetComponent<Room>();
            Doorway[] doors = BuildMe.GetExits();
            foreach (Doorway d in doors)
            {
                d.buildRoom();
            }
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
