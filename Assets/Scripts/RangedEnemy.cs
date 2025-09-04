using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.025f;
    private Rigidbody2D rb;
    public float distanceToShoot = 7f;
    public float chaseRadius = 7f;
    public float stopRadius = 4f;
    public float fireRate;
    private float timeToFire;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireForce = 20f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!target) 
        {
            GetTarget();
        }
        else 
        {
            RotateTowardsTarget();
        }

        if (target != null && Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            Shoot();   
        }

        if (target != null && Vector2.Distance(target.position, transform.position) <= stopRadius)
        {
            // if the target is within the stop radius, stop moving
            rb.velocity = Vector2.zero;
        }
        else if (target != null && Vector2.Distance(target.position, transform.position) <= chaseRadius)
        {
            // if the target is within the chase radius but outside the stop radius, set the enemy's velocity to move towards the target
            rb.velocity = transform.up * speed;
        }   
        else
        {
            // if the target is outside the chase radius, stop moving
            rb.velocity = Vector2.zero;
        }
    }

    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
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
}

