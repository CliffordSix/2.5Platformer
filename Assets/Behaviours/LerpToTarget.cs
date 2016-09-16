using UnityEngine;
using System.Collections;

public class LerpToTarget : MonoBehaviour {

    public Transform target;
    public float speed;
	
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

	// Update is called once per frame
	void FixedUpdate () {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed;
	}
}
