using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public float spawnRadius;
    public float spawnInterval;
    public Transform[] firePoints;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float detectionRadius;
    private bool canSpawnEnemies;
    private bool canFireBullets;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnEnemies());
        StartCoroutine(FireBullets());
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            return; // Exit the function if player is null
        }

        // Check if the player is within detection radius
        if (Vector2.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            // Allow the boss to spawn enemies and fire bullets
            canSpawnEnemies = true;
            canFireBullets = true;
        }
        else
        {
            // Don't allow the boss to spawn enemies and fire bullets
            canSpawnEnemies = false;
            canFireBullets = false;
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (canSpawnEnemies)
            {
                Vector2 spawnPosition1 = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
                Instantiate(enemyPrefab1, spawnPosition1, Quaternion.identity);

                Vector2 spawnPosition2 = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
                Instantiate(enemyPrefab2, spawnPosition2, Quaternion.identity);

                Vector2 spawnPosition3 = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
                Instantiate(enemyPrefab3, spawnPosition3, Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator FireBullets()
    {
        while (true)
        {
            if (canFireBullets)
            {
                foreach (Transform firePoint in firePoints)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.velocity = firePoint.up * bulletSpeed;
                }
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
}
