using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public struct Deck {

    public string name;
    public List<string> cards;
    public int index;

    public Deck(string name, int index)
    {
        this.name = name;
        this.index = index;
        cards = new List<string>();
    }

    public void AddCard(string name)
    {
        cards.Add(name);
    }

    public void RemoveCard(string name)
    {
        cards.Remove(name);
    }

    public void Shuffle()
    {
        List<string> old_cards = new List<string>(cards);
        cards.Clear();
        while (old_cards.Count > 0)
        {
            int index = Random.Range(0, old_cards.Count);
            string card = old_cards[index];
            old_cards.RemoveAt(index);
            cards.Add(card);
        }
    }

    public Card Draw()
    {
        string card_name = cards[0];
        return CardManager.it.Create(card_name);
    }
}
