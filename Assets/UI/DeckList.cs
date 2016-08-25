using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeckList : MonoBehaviour {

    public CollectionManager collection;
    public Transform content_container;
    public Transform preview_content_container;

    public GameObject deck_button_prefab;
    public float deck_button_offset = 0.15f;

    void Start()
    {
        LoadDecks();
    }

	void OnEnable()
    {
        LoadDecks();
    }

    void LoadDecks()
    {
        for (int i = collection.deck_collection.Count - 1; i >= 0; i--)
        {
            GameObject deck = collection.deck_collection[i];
            GameObject button = Instantiate<GameObject>(deck_button_prefab);

            button.GetComponentInChildren<Text>().text = deck.name;
            DeckButton script = button.GetComponent<DeckButton>();
            script.deck = deck.GetComponent<Deck>();
            script.card_preview_container = preview_content_container;
            button.transform.SetParent(content_container, false);
        }
    }
}
