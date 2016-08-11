using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Deck : MonoBehaviour {

    List<string> cards_ = new List<string>();

	public void AddCard(string name)
    {
        cards_.Add(name);
    }

    public void RemoveCard(string name)
    {
        cards_.Remove(name);
    }

    public void Shuffle()
    {
        List<string> old_cards = new List<string>(cards_);
        cards_.Clear();
        while (old_cards.Count > 0)
        {
            int index = Random.Range(0, old_cards.Count);
            string card = old_cards[index];
            old_cards.RemoveAt(index);
            cards_.Add(card);
        }
    }

    public Card Draw(CardManager manager)
    {
        string card_name = cards_[0];
        return manager.Create(card_name);
    }
}
