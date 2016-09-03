using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {

    public bool is2H = false;
    public Mesh Model;
    public Material weaponMat;

    public bool isRanged;

    public GameObject projectile;

   // public Sprite A1, A2, A3, A4;

    public List<Ability> WeaponAbilities = new List<Ability>();

    protected virtual void Update()
    {

    }

    public virtual void abilityOne(Transform Player)
    {
        Debug.Log("This Weapon is not for use");
        Instantiate(projectile, transform.parent.transform.position, Quaternion.identity);
    }

    public virtual void abilityTwo(Transform Player)
    {
        Debug.Log("This Weapon is not for use");
    }

    public virtual void abilityThree(Transform Player)
    {
        Debug.Log("This Weapon is not for use");
    }

    public virtual void abilityFour(Transform Player)
    {
        Debug.Log("This Weapon is not for use");
    }
}
