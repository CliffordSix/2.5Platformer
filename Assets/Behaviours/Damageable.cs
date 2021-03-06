﻿using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour {

    public GameObject Healthbar;
    public float maxHealth = 0.0f;
    public int maxArmour = 0;
    public float invincibilityPeriod = 0.0f;

    public float healthbarOffset = 3.0f;

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
        if (gameObject.layer == 14 && Healthbar != null)
        {
            Healthbar = Instantiate(Healthbar);
            Healthbar.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
        }
        else
            Healthbar = null;
    }

    public Vector2 GetScreenPosition(Transform transform, Canvas canvas)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
       
        Vector2 world_screenPosition = new Vector2(
        ((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
        ((viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));
        world_screenPosition.y += healthbarOffset;

        return world_screenPosition;
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float amount)
    {
        health = amount;
        if (health > maxHealth)
            health = maxHealth;
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
                
                float force = dynamicKnockback ? amount * 100 : staticKnockback;
                rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
      
    }

    void Update()
    {
        if(untilVulnerable > 0)
            untilVulnerable -= Time.deltaTime;

        if (health <= 0)
        {
            //Unit is dead drop a card destroy the unit
            if (gameObject.tag != "Player")
            {
                Destroy(Healthbar);
                Destroy(this.gameObject);
                DropManger.it.DropItem(transform.position);
            }
        }
    }

    void LateUpdate()
    {
        if (Healthbar != null)
        {
            Healthbar.GetComponent<RectTransform>().anchoredPosition = GetScreenPosition(transform, (GameObject.Find("Canvas").GetComponent<Canvas>()));
            Image[] bars = Healthbar.GetComponentsInChildren<Image>();
            foreach(Image i in bars)
            {
                if(i.gameObject.name == "HealthFull")
                {
                    i.fillAmount = health / maxHealth;
                }
            }
        }
    }
}