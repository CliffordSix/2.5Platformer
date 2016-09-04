using UnityEngine;
using System.Collections;

public class EnviroDamage : MonoBehaviour {

    public Vector2 knockbackPower = new Vector2(0, 5);

	// Use this for initialization
	void Start () {
	
	}
	
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
            PlayerController.it.damageable.Damage(10);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
