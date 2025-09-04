using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public static event Action<GameObject> PowerUpDestroyed; // Event raised when a power-up is destroyed

    private void OnDestroy()
    {
        PowerUpDestroyed?.Invoke(gameObject); // Raise the PowerUpDestroyed event
    }
}
