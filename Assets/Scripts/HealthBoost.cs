using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    [SerializeField] private int healthAmount = 50; // The amount of health restored by the power-up
    [SerializeField] private int maxHealthBoost = 0; // The amount of max health increased by the power-up

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(-healthAmount); // Use the TakeDamage function with negative damage to restore health
                playerHealth.maxHealth += maxHealthBoost; // Increase the player's maximum health
                Destroy(gameObject); // Destroy the power-up game object after it has been collected
            }
        }
    }
}
