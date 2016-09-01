using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeckList : MonoBehaviour {
    
    public Transform[] rows = new Transform[2];

    public GameObject deck_button_prefab;

    public DeckPreview previewer;
    public Button edit_button;

    Deck selected;

    public void SetSelected(Deck deck)
    {
        selected = deck;
        previewer.Preview(deck);
        edit_button.interactable = deck.index >= 0;
    }
    
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
        SetSelected(new Deck("", -1));
        for(int i = 0; i < 2; i++)
        {
            ClearRow(i);
        }
        for (int i = CollectionManager.it.decks.Count - 1; i >= 0; i--)
        {
            Deck deck = CollectionManager.it.decks[i];
            GameObject button = Instantiate<GameObject>(deck_button_prefab);

            button.GetComponentInChildren<Text>().text = deck.name;
            DeckButton script = button.GetComponent<DeckButton>();
            script.SetDeck(deck);
            script.deck_list = this;

            int row = i % 2 == 0 ? 0 : 1;
            button.transform.SetParent(rows[row], false);
        }
    }
}
