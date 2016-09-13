using UnityEngine;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour {

    public static MonsterManager it;

    public GameObject[] monsters;

    Dictionary<string, GameObject> monsterMap = new Dictionary<string, GameObject>();

    void Awake()
    {
        if (it == null)
        {
            it = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else if (it != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Init()
    {
        foreach(GameObject monster in monsters)
        {
            monsterMap.Add(monster.name, monster);
        }
    }

    public GameObject Create(string name)
    {
        return Instantiate(monsterMap[name]);
    }
}
