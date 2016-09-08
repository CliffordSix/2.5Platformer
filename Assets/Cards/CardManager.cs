using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class CardManager : MonoBehaviour
{
    public static CardManager it;

    public Card[] cards;

    Dictionary<string, Card> cardMap = new Dictionary<string, Card>();

    void Awake()
    {
        if(it == null)
        {
            it = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else if(it != this)
        {
            Destroy(gameObject);
        }
    }

    void Init()
    {
        foreach(Card card in cards)
        {
            cardMap.Add(card.name, card);
        }
    }

    public Card Get(string name)
    {
        return cardMap[name];
    }
}
