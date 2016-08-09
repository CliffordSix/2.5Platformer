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

    protected List<Behaviour> currentBehaviours = new List<Behaviour>();
    protected State currentState;
    public List<Behaviour> allBehaviours = new List<Behaviour>();

    protected virtual void Start()
    {
        allBehaviours.AddRange(idleBehaviours);
        allBehaviours.AddRange(chasingBehaviours);
        allBehaviours.AddRange(searchingBehaviours);
        allBehaviours.AddRange(fightingBehaviours);
     //   Debug.Log(fightingBehaviours.Count);
        foreach(Behaviour behaviour in allBehaviours)
        {
        //    Debug.Log(behaviour);
            behaviour.enabled = false;
        }

        SetState(initialState);
    }

    protected void SetState(State newState)
    {
        foreach (Behaviour behaviour in currentBehaviours)
        {
            behaviour.enabled = false;
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
            behaviour.enabled = true;
        }
    }
}
