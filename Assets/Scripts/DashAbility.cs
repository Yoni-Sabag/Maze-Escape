using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashDuration = 0.5f;
    public float dashSpeedMultiplier = 2f;
    public float dashCooldown = 1f;

    private bool isDashing = false;
    private Vector3 dashDirection;

    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;

        // Disable other movement while dashing
        playerController.enabled = false;

        // Get current movement direction
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        dashDirection = new Vector3(h, v, 0f).normalized;

        // Calculate dash endpoint
        Vector3 dashEnd = transform.position + dashDirection * dashDistance;

        // Set the player's speed to the dash speed
        playerController.moveSpeed *= dashSpeedMultiplier;

        // Move the player to the endpoint over a certain duration
        float t = 0f;
        while (t < dashDuration)
        {
            transform.position += dashDirection * playerController.moveSpeed * Time.deltaTime;
            t += Time.deltaTime;
            yield return null;
        }

        // Reset the player's speed and re-enable movement, then start the cooldown
        playerController.moveSpeed /= dashSpeedMultiplier;
        playerController.enabled = true;
        StartCoroutine(DashCooldown());
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }
}
