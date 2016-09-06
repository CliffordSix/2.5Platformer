﻿using UnityEngine;
using System.Collections;

public class Bow : Weapon {

	// Use this for initialization
	void Start () {
        is2H = true;
	}

    override public void abilityOne(Transform Player)
    {

        abilities[0].CallAbility(projectile);
    }

    override public void abilityTwo(Transform Player)
    {
        abilities[1].CallAbility(projectile);
    }

    override public void abilityThree(Transform Player)
    {
        abilities[2].CallAbility(projectile);
    }

    override public void abilityFour(Transform Player)
    {
        abilities[3].CallAbility(projectile);
    }

    Vector3 getSpawn(Vector3 p)
    {
        return new Vector3(p.x, p.y, 0.5f);
    }


}
