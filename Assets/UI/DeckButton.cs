using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeckButton : MonoBehaviour {

    public Deck deck;
    public DeckList deck_list;

    public void SetDeck(Deck deck)
    {
        this.deck = deck;
        Debug.Log(deck.name);
        GetComponentInChildren<Text>().text = deck.name;
    }

	public void Select()
    {
        deck_list.SetSelected(deck);
    }
}
