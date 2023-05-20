using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour, IDamageable, ICollisive
{
    [SerializeField] Transform healthBarPrefab;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] int maxHealth = 30;
    CameraShake cameraShake;
    HitEffects hitEffects;
    HealthSystem healthSystem;
    Shooter shooter;
    Vector2 inputValue = Vector2.zero;
    Rigidbody2D myRigidBody;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
        hitEffects = GetComponent<HitEffects>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    void Start()
    {
        Transform healthBarTransform = Instantiate(healthBarPrefab, new Vector3(0, 0.1f), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthSystem = new HealthSystem(maxHealth);

        healthBar.Setup(healthSystem, this.transform);
        healthBar.OnHealthReachesZero += HealthBar_OnHealthReachesZero;
    }

    void HealthBar_OnHealthReachesZero(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        Move();
        RotateSpriteToFollowMouse();
    }

    void RotateSpriteToFollowMouse()
    {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 mousePos = Input.mousePosition - playerPos;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.RotateTowards(
            this.transform.rotation,
            Quaternion.Euler(0, 0, angle + 270),
            rotateSpeed);
    }

    void Move() => myRigidBody.velocity += inputValue * moveSpeed;

    void OnMove(InputValue value) => inputValue = value.Get<Vector2>();

    void OnFire(InputValue value) => shooter.IsFiring = value.isPressed;

    public void TakeDamage() => Damaged(10);

    public void Collided() => Damaged(5);

    void Damaged(int value)
    {
        healthSystem?.Damage(value);
        hitEffects?.PlayHitExpossionEffect();
        cameraShake?.Play();
    }
}
