using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem
{
    public event EventHandler<int> OnHealthChange;
    private int health;
    private int maxHealth;
    public int Health => health;

    public HealthSystem(int newHealth)
    {
        health = newHealth;
        maxHealth = health;
    }

    public float GetHealthPercent() => health / (float)maxHealth;

    void ChangeHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        OnHealthChange?.Invoke(this, health);
    }

    public void Damage(int damageAmount) => ChangeHealth(-damageAmount);

    public void Heal(int healAmount) => ChangeHealth(healAmount);
}
