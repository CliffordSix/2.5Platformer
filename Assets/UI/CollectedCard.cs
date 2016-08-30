using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectedCard : MonoBehaviour {

    public DeckPreview previewer;
    string card;

    public void SetCard(string card, int amount)
    {
        this.card = card;
        transform.Find("Name").GetComponent<Text>().text = card;
        transform.Find("Count").GetComponent<Text>().text = "x" + amount.ToString();
    }

	public void AddToPreview()
    {
        previewer.AddCard(card);
    }
}
