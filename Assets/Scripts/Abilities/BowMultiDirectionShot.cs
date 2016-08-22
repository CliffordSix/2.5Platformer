using UnityEngine;
using System.Collections;

public class BowMultiDirectionShot : Ability{

    public override void CallAbility(GameObject projectile)
    {
        base.CallAbility(projectile);
        //  Time.timeScale = 0.05f;
        Vector3 spawn = getSpawn(Player.transform.position);
        Vector3 D = new Vector3(0, 0, 0);
        Vector3 lScale = Player.localScale;
        lScale.z = 0;
        int step = 45;
        Physics2D.IgnoreLayerCollision(13, 13);
        for (int i = 0; i < 7; i++)
        {

            GameObject p = Instantiate(projectile, spawn + (lScale / 2), Quaternion.Euler(new Vector3(0, 0, i * step))) as GameObject;
            D.x = 5 * Mathf.Cos((i * step));
            D.y = 5 * Mathf.Sin((i * step));
            D.x = Mathf.Rad2Deg * D.x;
            D.y = Mathf.Rad2Deg * D.y;
            D.z = 0;
            p.GetComponentInChildren<Projectile>().Dir = D;
            p.GetComponentInChildren<Projectile>().ProjectileForce = 0.05f;
            //  p.GetComponentInChildren<Rigidbody2D>().isKinematic = true;
        }
    }

    Vector3 getSpawn(Vector3 p)
    {
        return new Vector3(p.x, p.y, 0.5f);
    }
}
