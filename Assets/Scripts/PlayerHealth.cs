using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (gameObject != null)
        {
            currentHealth -= damage;

            if(currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        if (gameObject != null)
        {
            // Implement death behavior here
            Destroy(gameObject);
        }
    }
}

