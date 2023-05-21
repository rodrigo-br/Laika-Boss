using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireCooldown = 0.2f;
    float fireCooldownDefaultValue;
    bool finishfireCooldown = true;
    bool isFiring;
    public bool IsFiring
    {
        get { return isFiring; }
        set
        {
            isFiring = value;
            if (isFiring && finishfireCooldown)
            {
                StartCoroutine(FireCoroutine());
            }
        }
    }

    void Start()
    {
        fireCooldownDefaultValue = fireCooldown;
    }

    IEnumerator FireCoroutine()
    {
        finishfireCooldown = false;
        while (isFiring)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, transform.rotation);
            yield return new WaitForSecondsRealtime(fireCooldown);
        }
        finishfireCooldown = true;
    }

    public void BuffFireCooldown()
    {
        fireCooldown /= 2f;
    }

    public void ResetFireCooldown() => fireCooldown *= 2f;
}
