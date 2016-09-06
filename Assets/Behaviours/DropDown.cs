using UnityEngine;
using System.Collections;

public class DropDown : MonoBehaviour {

    public Trigger trigger;
    public string platformLayerName;
    public float dropTime = 1.0f;

    int platformLayer;
    float sinceIgnored = 0.0f;
    bool lastTriggerActive = false;
    bool ignoring = false;

    void Start()
    {
        platformLayer = LayerMask.NameToLayer(platformLayerName);
    }
	
    void SetIgnore(bool ignore)
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, platformLayer, ignore);
        ignoring = ignore;
    }

	// Check trigger to start/stop ignoring
	void Update () {
        bool triggerActive = trigger.IsActive();
        //If trigger just activated, start ignoring
	    if(triggerActive && !lastTriggerActive)
        {
            Debug.Log("true");
            SetIgnore(true);
            sinceIgnored = 0.0f;
        }

        //If ignoring, increment time ignored 
        if(ignoring)
        {
            sinceIgnored += Time.deltaTime;
            //If time ignored more than drop time, turn collisions back on
            if(sinceIgnored >= dropTime)
            {
                SetIgnore(false);
            }
        }

        lastTriggerActive = triggerActive;
	}
}
