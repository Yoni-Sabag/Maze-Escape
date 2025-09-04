using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int startingHealth = 5000;
    public int currentHealth = 5000;
    public int shieldHealth = 5000;

    public GameObject winText;

    private bool shieldEnabled = true; // Track whether the shield is currently enabled

    private void Start()
    {
        currentHealth = startingHealth;
        shieldHealth = startingHealth;
        winText.SetActive(false);
    }

    public void TakeDamage(int damageAmount)
    {
        if (shieldEnabled && shieldHealth > 0) // Check if the shield is active and enabled
        {
            shieldHealth -= damageAmount;
            if (shieldHealth <= 0)
            {
                // Shield is depleted, disable shield
                shieldEnabled = false;
            }
        }
        else
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        // Handle boss death
        Destroy(gameObject);
        winText.SetActive(true);
    }

    public void DisableShield()
    {
        if (shieldHealth > 0)
        {
            shieldHealth = 0;
            shieldEnabled = false;
        }
    }
}

