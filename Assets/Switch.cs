using UnityEngine;
using UnityEngine.Events;
using System.Collections;


namespace Behaviours
{
    [RequireComponent(typeof(Triggers.Collider))]
    public class Switch : MonoBehaviour
    {
        
        public bool _powered;

        [SerializeField]
        UnityEvent powered;
        [SerializeField]
        UnityEvent unpowered;

        public Triggers.Collider trigger { get; set; }
        bool lastTriggered = false;

        // Use this for initialization
        void Start()
        {
            trigger = GetComponent<Triggers.Collider>();

            if (_powered)
                powered.Invoke();
            else
                unpowered.Invoke();
        }

        // Update is called once per frame
        void Update()
        {
            if (trigger.IsActive() != lastTriggered)
            {
                if (trigger.IsActive())
                {
                    powered.Invoke();
                  
                } 
                else
                {
                    unpowered.Invoke();
                }
            }
            lastTriggered = trigger.IsActive();
        }





    }
}
