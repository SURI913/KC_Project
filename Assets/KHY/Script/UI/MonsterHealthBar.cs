using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBar : MonoBehaviour
{
    public Slider healthBarSlider; // Unity UI Slider
    public Text healthText; // Unity UI Text

    private float maxHealth;

    public void Initialize(float maxHealth)
    {
        this.maxHealth = maxHealth;
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;
        UpdateHealthText();
    }

    public void UpdateHealth(float currentHealth)
    {
        healthBarSlider.value = currentHealth;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = $"Health: {healthBarSlider.value}/{maxHealth}";
    }
}