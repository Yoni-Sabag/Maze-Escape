using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // The speed at which the boss rotates

    private void Update()
    {
        // Rotate the boss around the Z-axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
