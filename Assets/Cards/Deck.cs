using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class Deck {

    public string name;
    public List<string> cards = new List<string>();
    public int index;

    int nextDraw = 0;

    public Deck(string name, int index)
    {
        this.name = name;
        this.index = index;
    }

    public Deck(Deck toCopy, bool copyDrawPosition = false)
    {
        this.name = toCopy.name;
        this.index = toCopy.index;

        foreach (string card in toCopy.cards)
            this.AddCard(card);

        if (copyDrawPosition)
            this.nextDraw = toCopy.nextDraw;
    }

    public void AddCard(string name)
    {
        cards.Add(name);
    }

    public void RemoveCard(string name)
    {
        cards.Remove(name);
    }

    public bool isEmpty()
    {
        return nextDraw >= cards.Count;
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
        if (nextDraw >= cards.Count)
            return null;

        string card_name = cards[nextDraw++];
        return CardManager.it.Get(card_name);
    }
}
