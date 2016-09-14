using UnityEngine;
using System.Collections;

namespace Behaviours
{
    [RequireComponent(typeof(Triggers.Collider))]
    public class Teleport : MonoBehaviour
    {
        public Triggers.Collider trigger { get; set; }
        public Transform endPoint;

        bool lastActive = false;

        void Start()
        {
            trigger = GetComponent<Triggers.Collider>();
        }

        // Update is called once per frame
        void Update()
        {
            if (trigger.IsActive() && !lastActive)
                trigger.other.transform.position = endPoint.position;
            
            lastActive = trigger.IsActive();
        }
    }
}
