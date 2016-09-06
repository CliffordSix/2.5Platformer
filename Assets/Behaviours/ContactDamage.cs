using UnityEngine;
using System.Collections;

namespace Behaviours
{
    public class ContactDamage : MonoBehaviour
    {
        public Triggers.Collider trigger;
        public float damage = 0.0f;
        public float delay = 0.0f;

        float sinceLastDamage = 0.0f;

        // Update is called once per frame
        void Update()
        {
            if (trigger.IsActive())
            {
                if (sinceLastDamage <= 0.0f)
                {
                    Damageable damageable = trigger.other.GetComponent<Damageable>();
                    if (damageable == null)
                        damageable = trigger.other.GetComponentInChildren<Damageable>();
                    if (damageable == null)
                        damageable = trigger.other.GetComponentInParent<Damageable>();
                    if (damageable != null)
                    {
                        damageable.Damage(damage, transform);
                    }
                }
                sinceLastDamage = delay;
            }
            else
            {
                sinceLastDamage = delay;
            }
        }
    }
}
