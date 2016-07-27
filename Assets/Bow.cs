using UnityEngine;
using System.Collections;

public class Bow : Weapon {

	// Use this for initialization
	void Start () {
        is2H = true;
	}

    override public void abilityOne()
    {
        Debug.Log("Cast Ability 1");
    }

    override public void abilityTwo()
    {
        Debug.Log("Cast Ability 2");
    }

    override public void abilityThree()
    {
        Debug.Log("Cast Ability 3");
    }

    override public void abilityFour()
    {
        Debug.Log("Cast Ability 4");

    }
}
