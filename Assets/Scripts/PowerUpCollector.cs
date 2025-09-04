using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpCollector : MonoBehaviour
{
    public List<GameObject> powerUps = new List<GameObject>();
    private BossHealth bossHealth;
    public Text powerUpCountText;

    private void Start()
    {
        bossHealth = FindObjectOfType<BossHealth>();
        powerUpCountText = GameObject.Find("PowerUpCountText").GetComponent<Text>();
        UpdatePowerUpText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collect"))
        {
            powerUps.Add(other.gameObject);
            Destroy(other.gameObject);

            if (powerUps.Count == 6)
            {
                if (bossHealth != null)
                {
                    bossHealth.DisableShield();
                }
            }

            UpdatePowerUpText();
        }
    }

    private void UpdatePowerUpText()
    {
        if (powerUps.Count == 6)
        {
            powerUpCountText.text = "Shield Disabled!!!";
        }
        else
        {
            powerUpCountText.text = "Power-ups: " + powerUps.Count.ToString() + "/6";
        }
    }
}

