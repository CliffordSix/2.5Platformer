using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {

    protected Transform Player;
    public Sprite Icon;
    public float CD;

    public virtual void CallAbility(GameObject projectile)
    {
        
    }

}
