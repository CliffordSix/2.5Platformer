using UnityEngine;
using System.Collections;

public class GreatswordShockwave : Ability {

    public ParticleSystem Slash;

    public override void CallAbility(GameObject projectile)
    {
        Debug.Log("GreatswordSlash");
        
    }
}
