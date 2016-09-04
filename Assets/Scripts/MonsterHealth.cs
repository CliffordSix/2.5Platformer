using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {


	public float Health;
	public float Armour;
	public float Dmg;
	public Vector2 knockbackPower = new Vector2(0, 5);

    void start()
    {
        Physics2D.IgnoreLayerCollision(10, 14, true);
    }

	public void ApplyDamage(float dmg)
	{
		Health -= (dmg - (dmg / (100 / Armour)));
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player") {
            PlayerController.it.damageable.Damage(Dmg);
            Rigidbody2D eR = GetComponent<Rigidbody2D>();
            eR.velocity = new Vector2(0, 0);
		}
    }

	void Update()
	{
		if (Health <= 0) {
            GameObject.FindGameObjectWithTag("DropManager").GetComponent<DropManger>().DropItem(transform.position);
			Destroy (gameObject);
		}
	}


}
