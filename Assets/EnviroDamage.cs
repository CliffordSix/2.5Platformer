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
        {
            //Damage Player
            Camera.main.GetComponent<PlayerController>().Health -= 10;
            Rigidbody2D pR = col.transform.parent.GetComponent<Rigidbody2D>();
            pR.velocity = new Vector2(0, 0);
            pR.AddForce(knockbackPower, ForceMode2D.Impulse);
        }
    }

    

	// Update is called once per frame
	void Update () {
	
	}
}
