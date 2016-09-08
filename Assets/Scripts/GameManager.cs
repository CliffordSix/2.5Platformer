using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager it;

    Deck deck;
    float cardDelay = 1;
    float untilNextCard = 0.0f;

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

    void CastCard()
    {
        Room room = PlayerController.it.GetComponent<RoomTracker>().current;
        
    }

	// Update is called once per frame
	void Update () {
        untilNextCard -= Time.deltaTime;

	    if(untilNextCard <= 0.0f)
        {
            CastCard();
            untilNextCard = cardDelay;
        }
	}
}
