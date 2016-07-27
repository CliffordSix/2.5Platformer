using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum State
{
    Idle,
    Chasing,
    Searching,
    Fighting
}

public class StateManager : MonoBehaviour {

    public Transform target;
    public State initialState;
    public List<Behaviour> idleBehaviours = new List<Behaviour>();
    public List<Behaviour> chasingBehaviours = new List<Behaviour>();
    public List<Behaviour> searchingBehaviours = new List<Behaviour>();
    public List<Behaviour> fightingBehaviours = new List<Behaviour>();

    protected List<Behaviour> currentBehaviours;
    protected State currentState;

    void Start()
    {
        SetState(initialState);
    }

    protected void SetState(State newState)
    {
        foreach (Behaviour behaviour in currentBehaviours)
        {
            behaviour.gameObject.SetActive(false);
        }

        switch (newState)
        {
            case State.Idle:
                currentBehaviours = idleBehaviours;
                break;

            case State.Chasing:
                currentBehaviours = chasingBehaviours;
                break;

            case State.Searching:
                currentBehaviours = searchingBehaviours;
                break;

            case State.Fighting:
                currentBehaviours = fightingBehaviours;
                break;

            default:
                currentBehaviours = new List<Behaviour>();
                break;
        }

        currentState = newState;
        foreach (Behaviour behaviour in currentBehaviours)
        {
            behaviour.gameObject.SetActive(true);
        }
    }
}
