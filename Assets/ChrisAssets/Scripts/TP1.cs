using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP1 : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 300;

    public HealthBar healthBar;

    // For Test Only
    public int hp_Change = -30;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // For Test Only
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentHealth += hp_Change;
            healthBar.UpdateHealth(currentHealth);
        }
    }
}
