using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, ICollisive
{
    HealthSystem healthSystem;
    HitEffects hitEffects;

    void Awake()
    {
        hitEffects = GetComponent<HitEffects>();
    }
    void Start()
    {
        healthSystem = new HealthSystem(20);

        healthSystem.OnHealthChange += HealthSystem_OnHealthChanged;
    }

    void HealthSystem_OnHealthChanged(object sender, int health)
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage() => Damaged(10);

    public void Collided() => Damaged(20);

    void Damaged(int value)
    {
        healthSystem.Damage(value);
        hitEffects.PlayHitExpossionEffect();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ICollisive collisive = other.GetComponent<ICollisive>();
        if (collisive != null)
        {
            collisive.Collided();
            this.Collided();
        }
    }

    void OnDisable()
    {
        healthSystem.OnHealthChange -= HealthSystem_OnHealthChanged;
    }
}
