using UnityEngine;
using System.Collections;

public class Item : Collectible {

    public int Durability = 100;


    public void repairItem()
    {

        if(Durability < 100)
        {
            Durability = 100;
        }
        else
        {
            Debug.Log("Item doesn't need repairing");
        }
    }
    public int getDurability()
    {
        return Durability;
    }

}
