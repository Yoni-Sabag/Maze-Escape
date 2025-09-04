using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth PlayerHealth;
    public Image fillImage;
    public Text healthText;
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();     
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
            healthText.text = "DEAD";
        }

        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        
        float fillValue = (float)PlayerHealth.currentHealth / PlayerHealth.maxHealth;
        slider.value = fillValue; 
        healthText.text = "Health: " + PlayerHealth.currentHealth.ToString();    
    }
}

