using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour {

    public bool Up ,Down,Left ,Right;

    public bool collided;

	public Transform tLeft, bRight;

	public LayerMask roomLayer;

    public int Width, Height;
    float spawnModifier = 0.15f;

    public SpawnArea[] flyingSpawns;
    public SpawnArea[] groundSpawns;

    Dictionary<string, Doorway> doors = new Dictionary<string, Doorway>();

    void Start()
    {
    }

    public void FindDoors()
    {
        if (doors.Count > 0) return;
        foreach (Doorway door in GetComponentsInChildren<Doorway>())
        {
            doors.Add(door.dir, door);
        }
    }

    public Doorway GetExit(string direction)
    {
        try { return doors[direction]; }
        catch (System.Exception e)
        { }
        return null;
        
    }

    public List<Doorway> GetExits()
    {
        Doorway[] doorways = GetComponentsInChildren<Doorway>();
        List<Doorway> result = new List<Doorway>();
        foreach(Doorway door in doorways)
        {
            if (!door.Connected)
                result.Add(door);
        }
        return result;
    }

	bool isOverlapping()
	{
		return Physics2D.OverlapArea (tLeft.position, bRight.position, roomLayer);
	}

	public bool checkCollision()
	{
        //Debug.Log ("Checking for overlapping rooms");
        GetComponent<BoxCollider2D>().enabled = false;
        bool result = !isOverlapping();
        GetComponent<BoxCollider2D>().enabled = true;
        return result;
    }

    public int GetSpawnCount()
    {
        return (int)Mathf.Ceil((Width * Height) * spawnModifier);
    }

    public Vector3 GetRandomSpawn(GameObject monster)
    {
        SpawnArea[] spawns = new SpawnArea[0];
        if (monster.GetComponent<Behaviours.GroundFollow>() != null)
            spawns = groundSpawns;
        else if (monster.GetComponent<Behaviours.FlyingFollow>() != null)
            spawns = flyingSpawns;
        
        if(spawns.Length == 0 || spawns[0] == null)
            return transform.position + new Vector3(Width * 0.5f, Height * 0.5f);

        int index = Random.Range(0, spawns.Length);
        Vector3 pos = spawns[index].GetRandomPosition();
        pos.z = 0;
        return pos;
    }
}
