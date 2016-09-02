using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public Collider2D solid_collider;

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        Physics2D.IgnoreCollision(other, solid_collider, true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        Physics2D.IgnoreCollision(other, solid_collider, false);
    }
}
