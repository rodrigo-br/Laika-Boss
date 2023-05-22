using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioClip[] shootingClip;
    [SerializeField] AudioClip[] explosionClip;
    [SerializeField] AudioClip healClip;
    [SerializeField] AudioClip reloadClip;
    [Range(0f, 1f)][SerializeField] float sfxVolume = 1f;

    void PlayClip(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, sfxVolume);
    }

    public void PlayShootingClip()
    {
        if (shootingClip != null)
        {
            PlayClip(shootingClip[Random.Range(0, shootingClip.Length)]);
        }
    }

    public void PlayExplosionClip()
    {
        if (explosionClip != null)
        {
            PlayClip(explosionClip[Random.Range(0, explosionClip.Length)]);
        }
    }

    public void PlayReloadClip()
    {
        if (reloadClip != null)
        {
            PlayClip(reloadClip);
        }
    }

    public void PlayHealClip()
    {
        if (healClip != null)
        {
            PlayClip(healClip);
        }
    }
}
