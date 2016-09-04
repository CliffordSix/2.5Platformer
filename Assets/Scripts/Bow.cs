using UnityEngine;
using System.Collections;

public class Bow : Weapon {

	// Use this for initialization
	void Start () {
        is2H = true;
	}

    override public void abilityOne(Transform Player)
    {
        /*  Vector3 spawn = getSpawn(Player.transform.position);
          Vector3 v2 = Input.mousePosition;
          v2.z = 0;
          Vector3 D = (Input.mousePosition - Camera.main.WorldToScreenPoint(spawn + (Player.localScale / 2))).normalized;
          Vector3 objPos = Camera.main.WorldToScreenPoint(Player.transform.position);
          v2.x = v2.x - objPos.x;
          v2.y = v2.y - objPos.y;
          float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
          Vector3 lScale = Player.localScale;
          lScale.z = 0;
          GameObject p = Instantiate(projectile, spawn + (lScale / 2), Quaternion.Euler(new Vector3(0,0,angle ))) as GameObject;       
          p.GetComponentInChildren<Projectile>().Dir = D;*/

        abilities[0].CallAbility(projectile);
    }

    override public void abilityTwo(Transform Player)
    {
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
        for (int i = 0; i < 20; i++)
        {
            GameObject p = Instantiate(projectile, spawn + (lScale / 2), Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
            D.x += Random.Range(0, 0.05f);
            D.y += Random.Range(0, 0.05f);
            p.GetComponentInChildren<Projectile>().Dir = D;
            if(i > 5)
            {
                p.GetComponentInChildren<Projectile>().Knockback = new Vector2(0, 0);
            }
            p.GetComponentInChildren<Projectile>().ProjectileForce = 5 + Random.Range(0, 3);
        }
    }

    override public void abilityThree(Transform Player)
    {
      //  Time.timeScale = 0.05f;
        Vector3 spawn = getSpawn(Player.transform.position);
        Vector3 D = new Vector3(0,0,0);
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

    override public void abilityFour(Transform Player)
    {
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
        p.transform.localScale = new Vector3(4, 4, 4);
        p.GetComponentInChildren<Projectile>().ProjectileForce = lScale.x * 10;
        p.GetComponentInChildren<Rigidbody2D>().gravityScale = 0.025f;
        p.GetComponentInChildren<Projectile>().Dir = D;
        p.GetComponentInChildren<Projectile>().UltimateArrow = true;

    }

    Vector3 getSpawn(Vector3 p)
    {
        return new Vector3(p.x, p.y, 0.5f);
    }


}
