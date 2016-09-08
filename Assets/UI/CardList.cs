using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardList : MonoBehaviour {
    
    public Transform[] rows = new Transform[2];
    public DeckPreview previewer;

    public GameObject collected_card_prefab;
    
    void OnEnable()
    {
        Setup();
    }

    void ClearRow(int index)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in rows[index]) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

    void Setup()
    {
        for (int i = 0; i < 2; i++)
        {
            ClearRow(i);
        }
        int nextRow = 0;
        foreach(var pair in CollectionManager.it.cards)
        {
            Card card = CardManager.it.Get(pair.Key);

            GameObject button = Instantiate<GameObject>(collected_card_prefab);
            button.transform.SetParent(rows[nextRow], false);

            button.transform.Find("Image").GetComponent<Image>().sprite = card.image;

            CollectedCard script = button.GetComponent<CollectedCard>();
            script.SetCard(card.name, pair.Value);
            script.previewer = previewer;
            nextRow++;
            if(nextRow > 1) nextRow = 0;
        }
    }
}
