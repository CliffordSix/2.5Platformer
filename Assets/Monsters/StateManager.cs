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

    public List<BehaviourOld> idleBehaviours = new List<BehaviourOld>();
    public List<BehaviourOld> chasingBehaviours = new List<BehaviourOld>();
    public List<BehaviourOld> searchingBehaviours = new List<BehaviourOld>();
    public List<BehaviourOld> fightingBehaviours = new List<BehaviourOld>();

    protected List<BehaviourOld> currentBehaviours = new List<BehaviourOld>();
    public State currentState;
    public List<BehaviourOld> allBehaviours = new List<BehaviourOld>();

    protected virtual void Start()
    {
        allBehaviours.AddRange(idleBehaviours);
        allBehaviours.AddRange(chasingBehaviours);
        allBehaviours.AddRange(searchingBehaviours);
        allBehaviours.AddRange(fightingBehaviours);
     //   Debug.Log(fightingBehaviours.Count);
        foreach(BehaviourOld behaviour in allBehaviours)
        {
        //    Debug.Log(behaviour);
            behaviour.enabled = false;
        }

        SetState(initialState);
    }

    protected void SetState(State newState)
    {
        foreach (BehaviourOld behaviour in currentBehaviours)
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
                currentBehaviours = new List<BehaviourOld>();
                break;
        }

        currentState = newState;
        foreach (BehaviourOld behaviour in currentBehaviours)
        {
            behaviour.enabled = true;
        }
    }
}
