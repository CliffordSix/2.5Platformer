using UnityEngine;
using System;

public class Collectible{

    protected UInt32 uniqueID;
    protected string Name;
    protected string Description;
    protected Sprite Image;

    protected void setName(string n)
    {
        Name = n;
    }

    protected string getName()
    {
        return Name;
    }

    protected string getDescription()
    {
        return Description;
    }

}
