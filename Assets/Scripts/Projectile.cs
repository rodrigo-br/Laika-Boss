using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 10f;
    Rigidbody2D myRigidBody;

    void Awake() => myRigidBody = GetComponent<Rigidbody2D>();

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
        Destroy(gameObject, 3f);
    }
}
