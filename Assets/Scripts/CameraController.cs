using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	// Update is called once per frame
	void LateUpdate () {
        PlayerController player = PlayerController.it;

        Vector3 pos = transform.position;
        pos.x = player.transform.position.x;
        pos.y = player.transform.position.y;
        pos.z = -5;

        transform.position = pos;
    }
}