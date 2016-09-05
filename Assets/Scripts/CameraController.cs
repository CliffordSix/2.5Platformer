using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public bool load = false;

    public float y_ = 0;
    public float z_ = -5;

    // Update is called once per frame
    void LateUpdate() {
        if (load)
        {
            PlayerController player = PlayerController.it;

            Vector3 pos = transform.position;
            pos.x = player.transform.position.x;
            pos.y = player.transform.position.y + y_;
            pos.z = z_;

            transform.position = pos;
        }
    }
}