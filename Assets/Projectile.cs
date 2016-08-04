using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	float DespawnTime = 2.0f;
    Rigidbody2D rB;
    public Vector2 Dir;
    public float ProjectileForce;

	void Start () {
        rB = GetComponent<Rigidbody2D>();
        rB.AddForce( Dir * ProjectileForce, ForceMode2D.Impulse);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer != 10)
        {
			if (col.gameObject.layer == 12 || col.gameObject.layer == 9 || col.gameObject.layer == 8)
            {
                rB.isKinematic = true;
				transform.GetComponent<BoxCollider2D> ().enabled = false;
				StartCoroutine (Despawn(DespawnTime));
            }
            else if (rB.isKinematic == false)
            {
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
 
	//	transform.LookAt (transform.position + new Vector3(0,0, rB.velocity.y));
		if(!rB.isKinematic)
			transform.right = rB.velocity;
    }

	IEnumerator Despawn(float time)
	{
		yield return new WaitForSeconds(time);
		Destroy (gameObject);   
	}
}
