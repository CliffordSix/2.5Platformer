﻿using UnityEngine;
using System.Collections;

public class SwordSingle : Weapon
{

    void Start()
    {
        is2H = false;
        projectile = null;
    }

    protected override void Update()
    {
        base.Update();
        WeaponAbilities[0].Update();      
    }

    override public void abilityOne(Transform Player)
    {
        WeaponAbilities[0].CallAbility(projectile);
    }

    override public void abilityTwo(Transform Player)
    {
        WeaponAbilities[1].CallAbility(projectile);
    }


}
