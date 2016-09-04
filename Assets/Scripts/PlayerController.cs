using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public static PlayerController it;

    public Damageable damageable;

    public Transform mainHandPos;
    public Transform offhandPos;
    Weapon mainHandWep;
    Weapon offhandWep;
    
	void Awake () {
	    if(it != this)
        {
            it = this;
            Init();
        }
	}
	
    void Init()
    {

    }

	// Update is called once per frame
	void Update () {
	
	}

    public void SetMainWeapon(Weapon weapon)
    {
        if(mainHandWep)
        {
            Destroy(mainHandWep);
            mainHandWep = null;
        }

        mainHandWep = weapon;
        weapon.transform.parent = mainHandPos;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }

    public Weapon GetMainWeapon()
    {
        return mainHandWep;
    }

    public void SetOffWeapon(Weapon weapon)
    {
        if (offhandWep)
        {
            Destroy(offhandWep);
            offhandWep = null;
        }

        offhandWep = weapon;
        weapon.transform.parent = offhandPos;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }

    public Weapon GetOffWeapon()
    {
        return offhandWep;
    }
}
