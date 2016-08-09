using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public bool is2H = false;

    public GameObject projectile;

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
