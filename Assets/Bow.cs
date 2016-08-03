using UnityEngine;
using System.Collections;

public class Bow : Weapon {

	// Use this for initialization
	void Start () {
        is2H = true;
	}

    override public void abilityOne(Transform Player)
    {
        Vector3 spawn = getSpawn(Player.transform.position);
        Vector3 v2 = Input.mousePosition;
        v2.z = 0;
        Vector3 D = (Input.mousePosition - Camera.main.WorldToScreenPoint(spawn + (Player.transform.right / 2))).normalized;
        Vector3 objPos = Camera.main.WorldToScreenPoint(Player.transform.position);
        v2.x = v2.x - objPos.x;
        v2.y = v2.y - objPos.y;
        float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        GameObject p = Instantiate(projectile, spawn + (Player.transform.right / 2), Quaternion.Euler(new Vector3(0,0,angle - 90))) as GameObject;
        Debug.Log(angle);
        p.GetComponentInChildren<Projectile>().Dir = D;
    }

    override public void abilityTwo()
    {
        Debug.Log("Cast Ability 2");
    }

    override public void abilityThree()
    {
        Debug.Log("Cast Ability 3");
    }

    override public void abilityFour()
    {
        Debug.Log("Cast Ability 4");

    }

    Vector3 getSpawn(Vector3 p)
    {
        return new Vector3(p.x, p.y, 0);
    }
}
