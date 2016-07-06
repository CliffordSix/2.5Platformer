using System;
using UnityEngine;


public class Card : Collectible{

    protected UInt16 Difficulty = 0;
   // private Effect effect;


    public Card(string n, string d, UInt32 ID, UInt16 dif)
    {
        Name = n;
        Description = d;
        uniqueID = ID;
        Difficulty = dif;

    } 

    public UInt16 getDifficulty()
    {
        return Difficulty;
    }


}
