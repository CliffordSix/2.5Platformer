using UnityEngine;
using System.Collections;


namespace Behaviours
{
    [RequireComponent(typeof(Triggers.Collider))]
    public class SpawnPortal : MonoBehaviour {

        public Triggers.Collider trigger { get; set; }

        public Teleport tp;
        bool lastActive = false;


        void Start() {

        }

        void Update() {
            if (trigger.IsActive() && !lastActive)
                tp.endPoint = transform;
        
        }
    }
}

