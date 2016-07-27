using UnityEngine;
using System.Collections;

public class Basic : StateManager {

    public float combatRange = 1.0f;
    public float searchTime = 1.0f;

    bool inSight = false;
    float untilStopSearch;

    protected override void Start()
    {
        base.Start();
        untilStopSearch = searchTime;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player"))
            return;

        inSight = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player"))
            return;

        inSight = false;
    }

    void Update()
    {
        if(inSight)
        {
            //In combat range and chasing
            if(Vector2.Distance(transform.position, target.position) <= combatRange)
            {
                SetState(State.Fighting);
            }
            //Now out of combat range, or just into sight range
            else
            {
                SetState(State.Chasing);
            }
        }
        //
        else if(currentState == State.Chasing)
        {
            SetState(State.Searching);
        }

        if(currentState == State.Searching)
        {
            if (untilStopSearch <= 0.0f)
            {
                untilStopSearch = searchTime;
                SetState(State.Idle);
            }
            else
            {
                untilStopSearch -= Time.deltaTime;
            }
        }
    }
}
