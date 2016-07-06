using UnityEngine;
using System.Collections;
using System.IO;

public class Model : MonoBehaviour
{

    public Deck deck = new Deck();

	// Use this for initialization
	void Start () {
	    
	}

    public void AddCardToDeck()
    {
        for (int i = 0; i < 100; i++)
        {
            Card c = new Card("MegaImp", "Its an Imp, cept he's mega!", 0, 6);

            if (!deck.addCard(c)) 
                Debug.Log("Deck is Full");

        }
        Debug.Log(deck.getSize());

    }

	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.D))
	    {
	        Debug.Log(deck.getDifficulty());
	    }
	}
}
