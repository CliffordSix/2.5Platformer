using UnityEngine;
using System.Collections;

public class Basic : StateManager {

    public float combatRange = 1.0f;

    bool inSight = false;

	void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        inSight = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        inSight = false;
    }

    void Update()
    {
        if(inSight)
        {
            //In combat range and chasing
            if(Vector2.Distance(transform.position, target.position) <= combatRange && currentState == State.Chasing)
            {
                SetState(State.Fighting);
            }
            //Now out of combat range, or just into sight range
            else if(currentState == State.Fighting || currentState == State.Searching)
            {
                SetState(State.Chasing);
            }
        }
        //
        else if(currentState == State.Chasing)
        {
            SetState(State.Searching);
        }
    }
}
