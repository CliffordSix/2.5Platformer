using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{

    public static RoomManager it;

    public Behaviours.Teleport SpawnTeleport;

    public int DungeonSize = 60;
    public GameObject Portal;
    List<Transform> spawnLocations = new List<Transform>();

    public GameObject[] RoomList = new GameObject[6];
    public GameObject OriginalRoom;

    public GameObject Wall, Floor;

    [Tooltip("No. of spawns = width*height*spawnModifier")]
    public float spawnModifier = 0.01f;

    bool CloseDoors = true;
    int createdCount = 0;

    List<Doorway> expandList = new List<Doorway>();
    List<Doorway> openDoors = new List<Doorway>();

    void Awake()
    {
        if (it != this)
        {
            it = this;
            Init();
        }
    }

    // Use this for initialization
    void Init()
    {
        Room OR = OriginalRoom.GetComponent<Room>();

        //Start out with all original doors in list to expand
        expandList = OR.GetExits();

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

    void SpawnMonsters(Room room)
    {
        Deck deck = GameManager.it.GetDeck();
        int spawnsLeft = room.GetSpawnCount();

        while (spawnsLeft-- > 0)
        {
            Card card = deck.Draw();
            if (card == null) break;

            GameObject monster = Instantiate(card.monster);
            Vector3 position = room.GetRandomSpawn(monster);
            monster.transform.position = position;
        }
    }

    void Expand(int index)
    {
        //Get the door to be expanded
        Doorway door = expandList[index];
        door.DungeonSize = DungeonSize;

        //Remove it so we never check this door again
        expandList.RemoveAt(index);

        //Attempt to build a room at the door
        GameObject room = door.BuildRoom();

        //If the build failed
        if (room != null)
        {
            createdCount++;
            expandList.AddRange(room.GetComponent<Room>().GetExits());
            if(GameManager.it.GetDeck() != null)
                SpawnMonsters(room.GetComponent<Room>());   
        }
        else
        {
            
            openDoors.Add(door);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If we haven't created all the rooms and there are doors to expand
        if ((GameManager.it.GetDeck() != null && !GameManager.it.GetDeck().isEmpty()) || (GameManager.it.GetDeck() == null && createdCount < DungeonSize && expandList.Count > 0))
        {
            //Expand the first door in the list
            Expand(0);
            GUIManager.it.LoadBarInc(1.0f / (DungeonSize + 1.0f));
        }
        else if (CloseDoors)
        {
            openDoors.AddRange(expandList);

            //Block off all doors
            foreach (Doorway door in openDoors)
            {
                door.BlockDoorway();
            }
            GUIManager.it.LoadBarInc(1.0F);
            CloseDoors = false;

            if (SpawnTeleport == null) return;
            GameObject[] potentialSpawns = GameObject.FindGameObjectsWithTag("PlayerSpawn");

            if (potentialSpawns.Length < 0) return;
            int rand = UnityEngine.Random.Range(0, potentialSpawns.Length - 1);

            SpawnTeleport.endPoint = potentialSpawns[rand].transform;
            
            GameObject p = Instantiate(Portal, SpawnTeleport.endPoint.position, Quaternion.identity) as GameObject;
            p.GetComponent<Behaviours.Teleport>().enabled = false;
        }
    }
}
