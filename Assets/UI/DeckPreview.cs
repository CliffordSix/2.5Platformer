using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeckPreview : MonoBehaviour {

    public GameObject card_preview_prefab;
    public CollectionManager collection;

    Deck current_deck;

    void OnEnable()
    {
        Clear();
    }

    public void Clear()
    {
        Preview(new Deck("test", -1));
    }

	public void Preview(Deck deck)
    {
        Transform content = GetComponent<ScrollRect>().content.transform;

        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in content) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        current_deck = deck;
        if (current_deck.cards == null) return;

        Dictionary<string, int> card_counts = new Dictionary<string, int>();
        foreach (string card_name in deck.cards)
        {
            GameObject card_preview = Instantiate<GameObject>(card_preview_prefab);
            card_preview.GetComponentInChildren<Text>().text = card_name;
            card_preview.transform.SetParent(content, false);
        }
    }

    public void AddCard(string card)
    {
        current_deck.AddCard(card);
        Preview(current_deck);
    }

    public void SaveDeck()
    {
        if(current_deck.index < collection.deck_collection.Count  && current_deck.index > 0)
        {
            collection.deck_collection[current_deck.index] = current_deck;
            Clear();
        }
        else
        {
            collection.CreateDeck(current_deck);
        }
    }
}
