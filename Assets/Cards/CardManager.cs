using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public struct CardDetail
{
    public int id;
    public string name;
    public string flavour_text;
}

public class CardManager : MonoBehaviour
{
    public string card_definition_folder_ = "Cards";
    public List<GameObject> cards_;

    void Start()
    {
        string path = Path.Combine("Assets", "Resources");
        path = Path.Combine(path, card_definition_folder_);
        string[] card_folders = Directory.GetDirectories(path);
        foreach (string card_folder in card_folders)
        {
            string card_folder_name = Path.GetFileName(card_folder);
            try
            {
                GameObject card = Load(card_folder_name, cards_.Count);
                cards_.Add(card);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.StackTrace);
            }
        }
    }

    //Create a card prefab from a text file and model found in the given directory, ready to be instantiated
    GameObject Load(string card_directory, int id)
    {
        Debug.Log("Loading Card from: " + card_directory);
        string path = Path.Combine(card_definition_folder_, card_directory);

        //Load text file with card details in
        TextAsset card_detail_text = Resources.Load<TextAsset>(Path.Combine(path, "details"));
        if (card_detail_text == null)
        {
            throw new System.Exception("Failed to load card details");
        }
        //Convert from JSON string to CardDetail object
        CardDetail card_detail = JsonUtility.FromJson<CardDetail>(card_detail_text.text);
        card_detail.id = id;

        //Load prefab/model for card (not the monster, the actual card)
        GameObject card_model_prefab = Resources.Load<GameObject>(Path.Combine(path, "model"));
        if (card_model_prefab == null)
        {
            throw new System.Exception("Failed to load card model");
        }

        GameObject card_model = GameObject.Instantiate(card_model_prefab);

        //Create a new GameObject to hold the card script and model underneath
        GameObject card = new GameObject(card_detail.name);
        card_model.transform.parent = card.transform;
        card.hideFlags = HideFlags.HideInHierarchy;
        card.SetActive(false);

        //Add a card script to the card
        Card card_script = card.AddComponent<Card>();
        card_script.model = card_model.transform;
        card_script.SetCardDetail(card_detail);

        return card;
    }

    public Card Get(int id)
    {
        return cards_[id].GetComponent<Card>();
    }
}
