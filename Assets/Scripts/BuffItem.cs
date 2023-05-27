using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BuffItem : MonoBehaviour
{
    SFXManager sfxManager;
    System.Action[] buffs;
    Player player;
    bool growing;
    float defaultScale;
    float defaultLightIntensity;

    void Awake()
    {
        buffs = new System.Action[] {
            FireSpeed,
            Heal,
        };
        sfxManager = FindObjectOfType<SFXManager>();
    }

    void Start()
    {
        defaultScale = this.transform.localScale.x;
    }

    void FixedUpdate()
    {
        if (growing)
        {
            this.transform.localScale += (Vector3.one * 0.01f);
            if (this.transform.localScale.x > defaultScale)
            {
                growing = false;
            }
        }
        else
        {
            this.transform.localScale -= (Vector3.one * 0.01f);
            if (this.transform.localScale.x < defaultScale - 0.3f)
            {
                growing = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<Player>();

        if (player != null)
        {
            buffs[Random.Range(0, buffs.Length)]();
            Destroy(gameObject);
        }
    }

    void Heal()
    {
        player.Heal();
        sfxManager.PlayHealClip();
    }

    void FireSpeed()
    {
        player.FireSpeed();
        sfxManager.PlayReloadClip();
    }
}
