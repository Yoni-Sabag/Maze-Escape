using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostAmount = 0.5f;  // Speed boost amount

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Get the PlayerController script from the player object
            PlayerController playerController = collision.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // Increase the player's move speed by the boost amount
                playerController.moveSpeed += boostAmount;
                
                // Destroy the speed boost object
                Destroy(gameObject);
            }
        }
    }
}
