using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager it;

    Deck deck;

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
    
	void Init () {
	    
	}

    public void SetDeck(Deck deck)
    {
        this.deck = deck;
        this.deck.Shuffle();
    }

    public Deck GetDeck()
    {
        return deck;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
