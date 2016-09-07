using UnityEngine;
using UnityEditor;
using System.Collections;

public class Damageable : MonoBehaviour {

    public float maxHealth = 0.0f;
    public int maxArmour = 0;
    public float invincibilityPeriod = 0.0f;

    float health = 0;
    int armour = 0;
    float untilVulnerable = 0.0f;

    public bool takesKnockback = true;
    public bool dynamicKnockback = false;
    public float staticKnockback = 750;
    public float knockbackAngle = 0.0f;

    new Rigidbody2D rigidbody;

    // Use this for initialization
    void Start()
    {
        health = maxHealth;
        armour = maxArmour;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public float GetHealth()
    {
        return health;
    }

    public void Heal(float amount)
    {
        health += amount;
    }

    public void Damage(float amount, Transform damager = null)
    {
        if (untilVulnerable <= 0 || invincibilityPeriod <= 0)
        {
            health -= amount;
            untilVulnerable = invincibilityPeriod;

            if (takesKnockback && damager != null && rigidbody != null)
            {
                float radians = knockbackAngle * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

                Rigidbody2D otherBody = damager.GetComponent<Rigidbody2D>();
                if (otherBody != null)
                    direction.x *= otherBody.velocity.x > 0 ? 1 : -1;

                Debug.Log(direction.x);
                float force = dynamicKnockback ? amount * 100 : staticKnockback;
                rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }

    void Update()
    {
        if(untilVulnerable > 0)
            untilVulnerable -= Time.deltaTime;
    }

    [CustomEditor(typeof(Damageable))]
    public class DamageableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Damageable script = (Damageable)target;

            EditorGUILayout.LabelField("Health", script.health.ToString());
            script.maxHealth = EditorGUILayout.FloatField("Max Health", script.maxHealth);
            script.invincibilityPeriod = EditorGUILayout.FloatField("Invincivility Period", script.invincibilityPeriod);

            script.takesKnockback = EditorGUILayout.Toggle("Take Knockback", script.takesKnockback);
            if(script.takesKnockback)
            {
                script.dynamicKnockback = EditorGUILayout.Toggle("Calculate Knockback Dynamically", script.dynamicKnockback);
                if(!script.dynamicKnockback)
                {
                    script.staticKnockback = EditorGUILayout.FloatField("Static Knockback", script.staticKnockback);
                    script.knockbackAngle = EditorGUILayout.FloatField("Knockback Angle", script.knockbackAngle);
                }
            }
        }
    }
}