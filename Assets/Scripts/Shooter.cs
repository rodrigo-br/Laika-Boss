using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] float fireCooldown = 0.3f;
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

}
