using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class SpawnArea : MonoBehaviour {

    new Collider2D collider;

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

	public Vector2 GetRandomPosition()
    {
        Vector2 offset = Vector2.zero;
        if(this.collider is BoxCollider2D)
        {
            BoxCollider2D collider = this.collider as BoxCollider2D;

            Vector2 extents = collider.size - (collider.size / 2);
            float x = Random.Range(extents.x * -1, extents.x);
            float y = Random.Range(extents.y * -1, extents.y);
            offset = new Vector2(x, y);
        }
        else if(this.collider is CircleCollider2D)
        {
            CircleCollider2D collider = this.collider as CircleCollider2D;

            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;

            offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
            offset *= Random.Range(0, collider.radius);
        }

        return (Vector2)transform.position + offset;
    }
}
