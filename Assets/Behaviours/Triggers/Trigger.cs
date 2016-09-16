using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public abstract class Trigger : MonoBehaviour {

    public bool oneShot = false;
    public bool inverted = false;

    [SerializeField]
    UnityEvent onActivated;
    [SerializeField]
    UnityEvent onDeactivated;

    bool lastResult = false;

    protected abstract bool CheckActive();

    protected virtual void LateUpdate()
    {
        bool active = IsActive();
        if (JustActivated())
            onActivated.Invoke();

        if (JustDeactivated())
            onDeactivated.Invoke();

        lastResult = active;
    }

    public bool IsActive()
    {
        bool result = CheckActive();
        if(oneShot)
        {
            if (oneShot && lastResult)
            {
                result = true;
            }
            else if (oneShot && !lastResult)
            {
                result = CheckActive();
            }
        }
        if (inverted) result = !result;
        
        return result;
    }

    public bool JustActivated()
    {
        bool active = IsActive();
        return active && lastResult != active;
    }

    public bool JustDeactivated()
    {
        bool active = IsActive();
        return !active && lastResult != active;
    }
}
