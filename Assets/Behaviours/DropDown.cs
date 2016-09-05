using UnityEngine;
using System.Collections;

public class DropDown : MonoBehaviour {

    public Trigger trigger;
    public LayerMask platformLayer;
	
	// Update is called once per frame
	void Update () {
	    if(trigger.IsActive())
        {
            Debug.Log("Ran");
            Physics2D.IgnoreLayerCollision(gameObject.layer, platformLayer.value);
        }
	}
}
