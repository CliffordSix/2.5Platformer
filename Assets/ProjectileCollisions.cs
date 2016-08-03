using UnityEngine;
using System.Collections;

public class ProjectileCollisions : MonoBehaviour {

    Rigidbody2D rB;
    // Use this for initialization
    void Start () {

        rB = transform.parent.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision");
        if (col.gameObject.layer != 10)
        {
            if (col.gameObject.layer == 12)
            {
                Debug.Log("collision with a wall");
               rB.isKinematic = true;
            }
            else if (rB.isKinematic == false)
            {
                Destroy(gameObject);
            }
        }
    }

}
