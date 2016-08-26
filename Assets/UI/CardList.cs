using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardList : MonoBehaviour {

    public CollectionManager collection;
    public Transform[] rows = new Transform[2];

    public GameObject collected_card_prefab;

    void Start () {
        Setup();
	}

    void OnEnable()
    {
        Setup();
    }

    void ClearRow(int index)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in rows[index]) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

    void Setup()
    {
        for (int i = 0; i < 2; i++)
        {
            ClearRow(i);
        }
        int nextRow = 0;
        foreach(var pair in collection.card_collection)
        {
            GameObject button = Instantiate<GameObject>(collected_card_prefab);
            button.transform.FindChild("Name").GetComponent<Text>().text = pair.Key;
            button.transform.FindChild("Count").GetComponent<Text>().text = "x" + pair.Value.ToString();
            button.transform.SetParent(rows[nextRow], false);
            nextRow++;
            if(nextRow > 1) nextRow = 0;
        }
    }
}
