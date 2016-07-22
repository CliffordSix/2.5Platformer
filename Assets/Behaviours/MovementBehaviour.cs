using UnityEngine;
using System.Collections;

public class MovementBehaviour : MonoBehaviour {

    protected Rigidbody2D body;

    public virtual void Start() {
        body = transform.GetComponent<Rigidbody2D>();
    }

    void Update() {
        
    }
}
