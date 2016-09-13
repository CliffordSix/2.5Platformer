using UnityEngine;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour {

    public LayerMask layerMask;

    List<Transform> transformsOnPlatform = new List<Transform>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(layerMask == (layerMask | (1 << collision.gameObject.layer)))
        {
            collision.transform.parent = transform;
            transformsOnPlatform.Add(collision.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(transformsOnPlatform.Contains(collision.transform))
        {
            transformsOnPlatform.Remove(collision.transform);
            collision.transform.parent = null;
        }
    }
}
