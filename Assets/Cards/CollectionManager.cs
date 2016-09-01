using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public struct DeckDefinition
{
    public string[] cards;
}

public class CollectionManager : MonoBehaviour {

    public static CollectionManager it;

    public string decks_folder = "Decks";

    public Dictionary<string, int> cards = new Dictionary<string, int>();
    public List<Deck> decks = new List<Deck>();

    void Awake()
    {
        if (it == null)
        {
            it = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else if (it != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Init () {
        LoadDecks();
        LoadCardCollection();
    }

    void LoadDeck(string path)
    {
        string name = Path.GetFileNameWithoutExtension(path);
        FileStream stream = new FileStream(path, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        Deck deck = (Deck)formatter.Deserialize(stream);
        stream.Close();
        decks.Add(deck);
    }

    void LoadDecks()
    {
        string path = Application.persistentDataPath;
        path = Path.Combine(path, decks_folder);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string[] deck_files = Directory.GetFiles(path);
        foreach (string deck_file in deck_files)
        {
            LoadDeck(deck_file);
        }
    }

    public void CreateDeck(Deck deck)
    {
        deck.index = decks.Count;
        deck.name = "deck" + deck.index.ToString();
        decks.Add(deck);
    }

    public void SaveDecks()
    {
        foreach(Deck deck in decks)
        {
            string path = Application.persistentDataPath;
            path = Path.Combine(path, decks_folder);
            path = Path.Combine(path, deck.name);
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, deck);
            stream.Close();
        }
    }

    void LoadCardCollection()
    {
        string path = Application.persistentDataPath;
        path = Path.Combine(path, "card_collection");

        if (!File.Exists(path))
            return;

        FileStream stream = new FileStream(path, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        cards = (Dictionary<string, int>)formatter.Deserialize(stream);
        stream.Close();
    }

    public void SaveCards()
    {
        string path = Application.persistentDataPath;
        path = Path.Combine(path, "card_collection");

        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, cards);
        stream.Close();
    }

    public void CollectCard(string name, int amount = 1)
    {
        if (name == null) return;
        if(cards.ContainsKey(name))
        {
            int count = cards[name] + amount;
            cards[name] = count;
        }
        else
        {
            cards.Add(name, amount);
        }
    }
}
