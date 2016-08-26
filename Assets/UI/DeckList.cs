using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeckList : MonoBehaviour {

    public CollectionManager collection;
    public Transform[] rows = new Transform[2];
    public Transform preview_content_container;

    public GameObject deck_button_prefab;

    void Start()
    {
        LoadDecks();
    }

	void OnEnable()
    {
        LoadDecks();
    }

    void ClearRow(int index)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in rows[index]) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

    void LoadDecks()
    {
        for(int i = 0; i < 2; i++)
        {
            ClearRow(i);
        }
        for (int i = collection.deck_collection.Count - 1; i >= 0; i--)
        {
            GameObject deck = collection.deck_collection[i];
            GameObject button = Instantiate<GameObject>(deck_button_prefab);

            button.GetComponentInChildren<Text>().text = deck.name;
            DeckButton script = button.GetComponent<DeckButton>();
            script.deck = deck.GetComponent<Deck>();
            script.card_preview_container = preview_content_container;
            int row = i % 2 == 0 ? 0 : 1;
            Debug.Log(row);
            button.transform.SetParent(rows[row], false);
        }
    }
}
