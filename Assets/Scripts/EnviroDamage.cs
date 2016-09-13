using UnityEngine;
using System.Collections;

public class EnviroDamage : MonoBehaviour {

    public Vector2 knockbackPower = new Vector2(0, 5);
    public float Damage;

	// Use this for initialization
	void Start () {
	
	}
	
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Player should take damage");
            PlayerController.it.damageable.Damage(Damage, transform);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
