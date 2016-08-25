using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeckButton : MonoBehaviour {

    public Deck deck;
    public Transform card_preview_container;
    public GameObject card_preview_prefab;

	public void Preview()
    {
        //Clear preview containers existing children
        List<GameObject> children = new List<GameObject>();
        foreach(Transform child in card_preview_container) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        foreach(string card_name in deck.cards)
        {
            GameObject card_preview = Instantiate<GameObject>(card_preview_prefab);
            card_preview.GetComponentInChildren<Text>().text = card_name;
            card_preview.transform.SetParent(card_preview_container, false);
        }
    }
}
