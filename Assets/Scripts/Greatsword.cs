using UnityEngine;
using System.Collections;

public class Greatsword : Weapon
{
    void Start()
    {
        is2H = true;
        projectile = null;
    }
   /* protected override void Update()
    {
        base.Update();
        abilities[0].Update();
    }*/

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

}
