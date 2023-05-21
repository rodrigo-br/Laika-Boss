using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : MonoBehaviour
{
    System.Action[] buffs;
    Player player;
    bool growing;
    float defaultScale;

    void Awake()
    {
        buffs = new System.Action[] {
            FireSpeed,
            Heal,
        };
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

    void Heal() => player.Heal();

    void FireSpeed() => player.FireSpeed();
}
