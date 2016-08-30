using UnityEngine;
using System.Collections;

public class CollectedCard : MonoBehaviour {

    public DeckPreview previewer;
    string card;

    public void SetCard(string card, int amount)
    {
        this.card = card;

    }

	public void AddToPreview()
    {
        //previewer.AddCard(card)
    }
}
