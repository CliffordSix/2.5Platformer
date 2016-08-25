using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeckListUI : MonoBehaviour {

    public CollectionManager collection;
    public Transform content_container;

    public GameObject deck_button_prefab;
    public float deck_button_offset = 0.15f;

	void OnEnable()
    {
        LoadDecks();
    }

    void LoadDecks()
    {
        for (int i = 0; i < collection.deck_collection.Count; i++)
        {
            GameObject deck = collection.deck_collection[i];
            GameObject button = Instantiate<GameObject>(deck_button_prefab);
            RectTransform button_rect = button.GetComponent<RectTransform>();
            float y_anchor_diff = button_rect.anchorMax.y - button_rect.anchorMin.y;
            float max_y_anchor = 0.96f - (deck_button_offset * i);
            float min_y_anchor = max_y_anchor - 0.1f;
            button_rect.anchorMax = new Vector2(button_rect.anchorMax.x, max_y_anchor);
            button_rect.anchorMin = new Vector2(button_rect.anchorMin.x, min_y_anchor);
            button.transform.SetParent(content_container, false);

            button.GetComponentInChildren<Text>().text = deck.name;
        }
    }
}
