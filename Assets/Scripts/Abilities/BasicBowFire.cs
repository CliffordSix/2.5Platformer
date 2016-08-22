using UnityEngine;
using System.Collections;

public class BasicBowFire : Ability {

    public override void CallAbility(GameObject projectile)
    {
        base.CallAbility(projectile);
        Vector3 spawn = getSpawn(Player.transform.position);
        Vector3 v2 = Input.mousePosition;
        v2.z = 0;
        Vector3 D = (Input.mousePosition - Camera.main.WorldToScreenPoint(spawn + (Player.localScale / 2))).normalized;
        Vector3 objPos = Camera.main.WorldToScreenPoint(Player.transform.position);
        v2.x = v2.x - objPos.x;
        v2.y = v2.y - objPos.y;
        float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        Vector3 lScale = Player.localScale;
        lScale.z = 0;
        GameObject p = Instantiate(projectile, spawn + (lScale / 2), Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
        p.GetComponentInChildren<Projectile>().Dir = D;
    }

    Vector3 getSpawn(Vector3 p)
    {
        return new Vector3(p.x, p.y, 0.5f);
    }


}
