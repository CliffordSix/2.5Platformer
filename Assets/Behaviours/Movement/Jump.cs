using UnityEngine;
using System.Collections;

public class Jump : MovementBehaviour {

    public float dirSwitchTimeMax;
    public float dirSwitchTimeMin;
    float untilDirSwitch = 0.0f;

    public float speed;
    public float jumpForce;
    float direction = -1;
    bool inAir = false;

    void UpdateDirection()
    {
        untilDirSwitch -= Time.deltaTime;
        if (untilDirSwitch <= 0)
        {
            direction *= -1;
            untilDirSwitch = Random.Range(dirSwitchTimeMin, dirSwitchTimeMax);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("collide");
        int mask = LayerMask.GetMask(new string[] {"Ground", "Platform"});
        int layer = 1 << coll.gameObject.layer;
        if((mask & layer) != 0)
        {
            inAir = false;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        inAir = true;
    }

    // Update is called once per frame
    void Update () {
        UpdateDirection();

        body.AddForce(new Vector2(direction * speed * Time.deltaTime, inAir ? 0.0f : jumpForce));
	}
}
