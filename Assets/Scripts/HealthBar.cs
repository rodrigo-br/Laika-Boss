using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBar : MonoBehaviour
{
    public event EventHandler OnHealthReachesZero;
    HealthSystem healthSystem;
    Transform target;

    public void Setup(HealthSystem newHealthSystem, Transform parent)
    {
        healthSystem = newHealthSystem;
        target = parent;
        
        healthSystem.OnHealthChange += HealthSystem_OnHealthChanged;
    }

    void HealthSystem_OnHealthChanged(object sender, int health)
    {
        transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
        if (health <= 0)
        {
            OnHealthReachesZero?.Invoke(this, System.EventArgs.Empty);
            Destroy(gameObject);
        }
    }

    void OnDisable()
    {
        healthSystem.OnHealthChange -= HealthSystem_OnHealthChanged;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + transform.up * 0.8f;
        }
    }
}
