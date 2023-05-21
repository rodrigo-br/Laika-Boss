using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, ICollisive
{
    [SerializeField] float rotateSpeed = 5f;
    HealthSystem healthSystem;
    HitEffects hitEffects;
    Player player;

    void Awake()
    {
        hitEffects = GetComponent<HitEffects>();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        healthSystem = new HealthSystem(20);

        healthSystem.OnHealthChange += HealthSystem_OnHealthChanged;
        if (player == null) { return; }
        Vector2 target = player.transform.position - this.transform.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle + 270);
        GetComponent<Shooter>().IsFiring = true;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            return ;
        }
        Vector2 target = player.transform.position - this.transform.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.RotateTowards(
            this.transform.rotation,
            Quaternion.Euler(0, 0, angle + 270),
            rotateSpeed * Time.deltaTime);
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
