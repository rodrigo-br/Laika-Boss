using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffects : MonoBehaviour
{
    [SerializeField] ParticleSystem hitExplosionEffect;
    SFXManager sfxManager;

    void Awake()
    {
        sfxManager = FindObjectOfType<SFXManager>();
    }

    public void PlayHitExpossionEffect()
    {
        if (!hitExplosionEffect)
        {
            return ;
        }
        sfxManager.PlayExplosionClip();
        ParticleSystem instance = Instantiate(hitExplosionEffect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }
}
