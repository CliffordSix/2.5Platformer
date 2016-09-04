using UnityEngine;
using UnityEditor;
using System.Collections;

public class Damageable : MonoBehaviour {

    public float maxHealth = 0.0f;
    float health = 0;

    public bool takesKnockback = true;
    public bool dynamicKnockback = true;
    public float staticKnockback = 0.0f;

    // Use this for initialization
    void Start()
    {
        health = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public float GetHealth()
    {
        return health;
    }

    public void Heal(float amount)
    {
        health += amount;
    }

    public void Damage(float amount)
    {
        health -= amount;
    }

    [CustomEditor(typeof(Damageable))]
    public class DamageableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Damageable script = (Damageable)target;

            script.health = EditorGUILayout.FloatField("Max Health", script.health);
            script.takesKnockback = EditorGUILayout.Toggle("Take Knockback", script.takesKnockback);
            if(script.takesKnockback)
            {
                script.dynamicKnockback = EditorGUILayout.Toggle("Calculate Knockback Dynamically", script.dynamicKnockback);
                if(!script.dynamicKnockback)
                {
                    script.staticKnockback = EditorGUILayout.FloatField("Static Knockback", script.staticKnockback);
                }
            }
        }
    }
}