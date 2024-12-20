using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class uiController : MonoBehaviour
{
    public TextMeshProUGUI levelUI; // Reference to the TextMeshPro component
    public int playerLevel = 1; // Example player's level variable

    public TextMeshProUGUI ammoUI; // Reference to the TextMeshPro component
    public int playerAmmo = 0; // Example player's level variable
    public int playerMagSize = 0; // Example player's level variable

    public Slider healthSlider; // Reference to the UI Slider
    public Gradient healthGradient; // Gradient to change color based on health
    public Image fillImage; // Reference to the Fill image in the Slider
    public int playerMaxHealth = 10;
    public int playerHealth = 10;

    void Start()
    {   
        // initialise the level display when the game starts
        UpdateLevelUI();
    }
    

    // Methods to update the TextMeshPro for the level, life and ammo display
    void UpdateAmmoUI()
    {
        ammoUI.text = playerAmmo + "/" + playerMagSize;
    }
    void UpdateLevelUI()
    {
        levelUI.text = "LV: " + playerLevel.ToString();
    }
    public void UpdateHealthUI()
    {
        SetMaxHealth();
        healthSlider.value = playerHealth;
        fillImage.color = healthGradient.Evaluate(healthSlider.normalizedValue); // Gradient color
    }
    public void SetMaxHealth()
    {
        healthSlider.maxValue = playerMaxHealth;
    }
}