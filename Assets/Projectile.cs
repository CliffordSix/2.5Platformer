using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    Rigidbody2D rB;
    public Vector2 Dir;
    public float ProjectileForce;
	// Use this for initialization
	void Start () {
        rB = GetComponent<Rigidbody2D>();
        rB.AddForce( Dir * ProjectileForce, ForceMode2D.Impulse);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer != 10)
        {
            if (col.gameObject.layer == 12)
            {
                rB.isKinematic = true;
            }
            else if (rB.isKinematic == false)
            {
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
 
      transform.parent.transform.LookAt(transform.parent.transform.position + new Vector3(rB.velocity.x, rB.velocity.y, 0));

    }

    // Update is called once per frame
    void Update () {

	}
}
