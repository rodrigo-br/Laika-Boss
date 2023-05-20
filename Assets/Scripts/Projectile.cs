using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    SFXManager sfxManager;
    [SerializeField] float projectileSpeed = 10f;
    Rigidbody2D myRigidBody;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        sfxManager = FindObjectOfType<SFXManager>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage();
            Destroy(gameObject);
        }
    }

    void Start()
    {
        myRigidBody.velocity = transform.up * projectileSpeed;
        sfxManager.PlayShootingClip();
        Destroy(gameObject, 3f);
    }
}
