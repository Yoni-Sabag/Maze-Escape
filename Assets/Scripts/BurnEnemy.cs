using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnEnemy : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.025f;
    public float knockbackForce = 7000f; // the force to apply to the player when hit
    public float chaseRadius = 5f; // the maximum distance at which the enemy will chase the player
    public int tickDamage = 5; // the damage dealt by each tick
    private Rigidbody2D rb;
    private int ticks = 0;
    private float timeBetweenHits;
    private float damageTickDuration = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ticks = 1;
        timeBetweenHits = damageTickDuration / (ticks - 1);
    }

    private void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        else if (target != null && Vector2.Distance(transform.position, target.position) <= chaseRadius)
        {
            RotateTowardsTarget();
        }
    }

    private void FixedUpdate()
    {
        if (target != null && Vector2.Distance(transform.position, target.position) <= chaseRadius)
        {
            rb.velocity = transform.up * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private IEnumerator ApplyDamageTick(PlayerHealth playerHealth)
    {
        for (int i = 0; i < 5; i++)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(tickDamage);
            }
            yield return new WaitForSeconds(0.5f);
        }

        target = null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // If this is the first tick, deal the full damage.
                if (ticks == 0)
                {
                    playerHealth.TakeDamage(tickDamage * 5);
                }
                else
                {
                    // Start the coroutine to apply the damage over time.
                    StartCoroutine(ApplyDamageTick(playerHealth));
                }

                ticks++;

                // Reset the tick counter after all ticks have occurred.
                if (ticks >= 5)
                {
                    ticks = 0;
                }

                Vector2 knockbackDirection = other.transform.position - transform.position;
                knockbackDirection.Normalize();
                Rigidbody2D playerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
                if (playerRigidbody != null)
                {
                    playerRigidbody.AddForce(knockbackDirection * knockbackForce);
                }
            }
        }
    }
}

