using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 50f; // The amount of damage the bullet will deal

    private void Start()
    {
        // Call the DestroyBullet function after 2 seconds
        Invoke("DestroyBullet", 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cancel the Invoke call since the bullet has hit something
        CancelInvoke("DestroyBullet");

        // Get the EnemyHealth or PlayerHealth component of the object that the bullet collided with
        BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        // If the object has an EnemyHealth component, deal damage to it
        if (bossHealth != null)
        {
            bossHealth.TakeDamage((int) damage);
        }
        
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage((int) damage);
        }

        // If the object has a PlayerHealth component, deal damage to it
        if (playerHealth != null)
        {
            playerHealth.TakeDamage((int) damage / 5);
        }

        // Destroy the bullet
        Destroy(gameObject);
    }

    private void DestroyBullet()
    {
        // Destroy the bullet after 2 seconds
        Destroy(gameObject);
    }
}



