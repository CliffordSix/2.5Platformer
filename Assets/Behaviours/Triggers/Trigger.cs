using UnityEngine;
using System.Collections;

public abstract class Trigger : MonoBehaviour {

    public bool oneShot = false;

    bool lastResult = false;

    protected abstract bool CheckActive();

    public bool IsActive()
    {
        if(oneShot)
        {
            if (oneShot && lastResult)
            {
                return true;
            }
            else if (oneShot && !lastResult)
            {
                lastResult = CheckActive();
                return lastResult;
            }
        }

        return CheckActive();
    }
}
