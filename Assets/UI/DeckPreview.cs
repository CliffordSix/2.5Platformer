using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeckPreview : MonoBehaviour {

    public GameObject card_preview_prefab;

	public void Preview(Deck deck)
    {
        Transform content = GetComponent<ScrollRect>().content.transform;

        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in content) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        foreach (string card_name in deck.cards)
        {
            GameObject card_preview = Instantiate<GameObject>(card_preview_prefab);
            card_preview.GetComponentInChildren<Text>().text = card_name;
            card_preview.transform.SetParent(content, false);
        }
    }
}
