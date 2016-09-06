using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public static PlayerController it;

    public Damageable damageable;

    public Transform mainHandPos;
    public Transform offhandPos;
    public Weapon mainHandWep;
    public Weapon offhandWep;
    
	void Awake () {
	    if(it != this)
        {
            it = this;
            Init();
        }
	}
	
    void Init()
    {
        SetMainWeapon(mainHandWep);
        if (!mainHandWep.is2H)
        {
            SetOffWeapon(offhandWep);
        }
        GUIManager.it.SetAbilities(mainHandWep, offhandWep);
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void SetMainWeapon(Weapon weapon)
    {
        mainHandWep = Instantiate(weapon, mainHandPos.position, mainHandPos.rotation, mainHandPos) as Weapon;
       
        /*
        if (mainHandWep)
        {
            Destroy(mainHandWep);
            mainHandWep = null;
        }

        mainHandWep = weapon;
       */
    }

    public Weapon GetMainWeapon()
    {
        return mainHandWep;
    }

    public void SetOffWeapon(Weapon weapon)
    {
        offhandWep = Instantiate(weapon, offhandPos.position, offhandPos.rotation, offhandPos) as Weapon;
      /*  if (offhandWep)
        {
            Destroy(offhandWep);
            offhandWep = null;
        }
        
        offhandWep = weapon;
        */
    }

    public Weapon GetOffWeapon()
    {
        return offhandWep;
    }
}
