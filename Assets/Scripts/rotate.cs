using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

    public float speed = 2;
    public Vector3 axis = Vector3.up;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(axis, speed);
	}
}
