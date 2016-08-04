using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {


	public float Health;
	public float Armour;
	public float Dmg;
	public Vector2 knockbackPower = new Vector2(0, 5);


	public void ApplyDamage(float dmg)
	{
		Health -= (dmg - (dmg / (100 / Armour)));
	}

	void OnCollisionEnter2D(Collider col)
	{
		if (col.tag == "Player") {
			Camera.main.GetComponent<PlayerController>().Health -= Dmg;
			Rigidbody2D pR = col.GetComponent<Rigidbody2D>();
			pR.velocity = new Vector2(0, 0);
			pR.AddForce(knockbackPower, ForceMode2D.Impulse);
		
		}
	}

	void Update()
	{
		if (Health <= 0) {
			Destroy (gameObject);
		}
	}


}
