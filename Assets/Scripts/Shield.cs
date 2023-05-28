using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable, ICollisive
{
    [SerializeField] int shieldHealth = 50;
    SpriteRenderer mySpriteRenderer;
    HitEffects hitEffects;
    HealthSystem healthSystem;
    Color increaseAlpha = new Color(0, 0, 0, 0.005f);
    float defaultAlpha;

    void Awake()
    {
        hitEffects = GetComponent<HitEffects>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        defaultAlpha = mySpriteRenderer.color.a;
        healthSystem = new HealthSystem(shieldHealth);
    }
    
    void OnEnable()
    {
        mySpriteRenderer.color = new Color(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b, 0);
        healthSystem.Heal(shieldHealth);
        StartCoroutine(IncreaseShieldAlphaCoroutine());
    }

    IEnumerator IncreaseShieldAlphaCoroutine()
    {
        while (mySpriteRenderer.color.a <= defaultAlpha)
        {
            mySpriteRenderer.color += increaseAlpha;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void Damaged(int value)
    {
        healthSystem.Damage(value);
        hitEffects.PlayHitExpossionEffect();
        if (healthSystem.Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int value)
    {
        Damaged(value);
    }

    public void Collided()
    {
        Damaged(10);
    }
}
