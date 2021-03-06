﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeckPreview : MonoBehaviour {

    public GameObject card_preview_prefab;

    Deck current_deck;

    void OnEnable()
    {
        Clear();
    }

    public void Clear()
    {
        Preview(new Deck("", -1));
    }

	public void Preview(Deck deck)
    {
        Transform content = GetComponent<ScrollRect>().content.transform;

        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in content) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        if (deck == null)
        {
            current_deck = null;
            return;
        }

        current_deck = new Deck(deck);
        if (current_deck.cards == null) return;
        
        foreach (string card_name in deck.cards)
        {
            Card card = CardManager.it.Get(card_name);

            GameObject card_preview = Instantiate<GameObject>(card_preview_prefab);
            card_preview.transform.Find("Image").GetComponent<Image>().sprite = card.image;

            card_preview.transform.Find("Name").GetComponent<Text>().text = card_name;
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
        if(current_deck.index < CollectionManager.it.decks.Count  && current_deck.index >= 0)
        {
            CollectionManager.it.decks[current_deck.index] = current_deck;
            Clear();
        }
        else
        {
            CollectionManager.it.CreateDeck(current_deck);
        }

        CollectionManager.it.SaveDecks();
    }
}
