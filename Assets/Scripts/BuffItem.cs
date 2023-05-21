using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : MonoBehaviour
{
    System.Action[] buffs;
    Player player;

    void Awake()
    {
        buffs = new System.Action[] {
            FireSpeed,
            Heal,
        };
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
