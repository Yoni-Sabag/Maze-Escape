using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReduce : MonoBehaviour
{
    public int reduceAmount = 25; // Amount of health to reduce from enemies

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Find all gameobjects with the "Enemy" tag

            foreach (GameObject enemy in enemies)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>(); // Get the EnemyHealth script of the enemy

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(reduceAmount); // Call the TakeDamage() method of the EnemyHealth script to reduce the enemy's health
                }
            }

            Destroy(gameObject); // Destroy the power-up after it has been collected
        }
    }
}
