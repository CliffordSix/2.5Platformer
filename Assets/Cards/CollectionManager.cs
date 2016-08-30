using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

[System.Serializable]
public struct DeckDefinition
{
    public string[] cards;
}

public class CollectionManager : MonoBehaviour {

    public string decks_folder = "Decks";

    public Dictionary<string, int> card_collection = new Dictionary<string, int>();
    public List<Deck> deck_collection = new List<Deck>();

	// Use this for initialization
	void Start () {
        LoadDecks();
        LoadCardCollection();
	}

    void LoadDeck(string name)
    {
        TextAsset deck_text = Resources.Load<TextAsset>(Path.Combine(decks_folder, name));
        if(deck_text == null)
        {
            throw new System.Exception("Failed to load deck: " + name);
        }
        DeckDefinition definition = JsonConvert.DeserializeObject<DeckDefinition>(deck_text.text);
        Deck deck = new Deck(name, deck_collection.Count);
        foreach(string card_name in definition.cards)
        {
            deck.AddCard(card_name);
        }
        deck_collection.Add(deck);
    }

    void LoadDecks()
    {
        string path = Path.Combine("Assets", "Resources");
        path = Path.Combine(path, decks_folder);
        string[] deck_files = Directory.GetFiles(path);
        foreach(string deck_file in deck_files)
        {
            if(Path.GetExtension(deck_file) == ".meta")
            {
                continue;
            }
            string deck_file_name = Path.GetFileNameWithoutExtension(deck_file);
            try
            {
                LoadDeck(deck_file_name);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.StackTrace);
            }
        }
    }

    void SaveDecks()
    {

    }

    void LoadCardCollection()
    {
        TextAsset card_collection_text = Resources.Load<TextAsset>("card_collection");
        if (card_collection_text == null)
        {
            throw new System.Exception("Failed to load card collection");
        }
        JObject card_collection_o = JObject.Parse(card_collection_text.text);
        foreach(var p in card_collection_o)
        {
            CollectCard(p.Key, p.Value.ToObject<int>());
        }
    }

    void SaveCardCollection()
    {

    }

    public void CollectCard(string name, int amount = 1)
    {
        if(card_collection.ContainsKey(name))
        {
            int count = card_collection[name] + amount;
            card_collection[name] = count;
        }
        else
        {
            card_collection.Add(name, amount);
        }
    }
}
