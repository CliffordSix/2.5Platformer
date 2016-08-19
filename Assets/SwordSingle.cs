using UnityEngine;
using System.Collections;

public class SwordSingle : Weapon
{

    void Start()
    {
        is2H = false;
        projectile = null;
    }

    override public void abilityOne(Transform Player)
    {
        Debug.Log("Sword Attack 1");
    }

    override public void abilityTwo(Transform Player)
    {
        Debug.Log("Sword Attack 2");
    }


}
