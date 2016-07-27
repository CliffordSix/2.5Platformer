using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public bool is2H = false;

    public GameObject projectile;

	public virtual void abilityOne()
    {
        Debug.Log("This Weapon is not for use");
        Instantiate(projectile, transform.parent.transform.position, Quaternion.identity);
    }

    public virtual void abilityTwo()
    {
        Debug.Log("This Weapon is not for use");
    }

    public virtual void abilityThree()
    {
        Debug.Log("This Weapon is not for use");
    }

    public virtual void abilityFour()
    {
        Debug.Log("This Weapon is not for use");
    }
}
