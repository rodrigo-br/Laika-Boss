using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    DimensionManager dimensionManager;
    SFXManager sfxManager;
    [SerializeField] float projectileSpeed = 10f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;

    void Awake()
    {
        dimensionManager = FindObjectOfType<DimensionManager>();
        myRigidBody = GetComponent<Rigidbody2D>();
        sfxManager = FindObjectOfType<SFXManager>();
        myAnimator = GetComponent<Animator>();
        if (myAnimator != null) 
        {
            myAnimator.SetBool("isMainDimension", dimensionManager.mainDimension);
        }
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
