using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class DeckOld
{

    List<Card> Cards = new List<Card>();
    UInt16 Difficulty = 0;
    string Name;
    //Default Deck Size is 10
    private UInt32 MaxSize = 100;
    private UInt32 Size = 0;

    public void setName(string n)
    {
        Name = n;
    }

    public string getName()
    {
        return Name;
    }

    public UInt16 getDifficulty()
    {
        return Difficulty;
    }

    public UInt32 getSize()
    {
        return Size;
    }

    private void calculateDifficulty()
    {
        UInt32 d = 0;

        for (int i = 0; i < Size; i++)
        {
            //d += Cards[i].getDifficulty();
        }

        Difficulty = (ushort)Mathf.FloorToInt(((float)d / (float)Size) * (Size / 10));

        //Assuming Max container size = 90 then the maximum difficulty is 50.
        /*
         * 0-10     Standard
         * 11-20    Tough
         * 21-30    Brutal
         * 31-40    Destroyer
         * 41+      Abyssal
         * Difficulty Ratings
         */
    }


    public bool addCard(Card c)
    {
        if (Size == MaxSize)
            return false;

        Cards.Add(c);
        Size++;
        calculateDifficulty();

        return true;
    }

    public bool RemoveCard(Card c)
    {
        Cards.Remove(c);
        Size--;

        return true;
    }


}
