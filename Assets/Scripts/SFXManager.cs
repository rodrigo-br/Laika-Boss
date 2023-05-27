using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioClip[] shootingClip;
    [SerializeField] AudioClip[] explosionClip;
    [SerializeField] AudioClip healClip;
    [SerializeField] AudioClip reloadClip;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider; 
    AudioSource myAudioSource;

    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }   

    void Start()
    {
        musicSlider.onValueChanged.AddListener(_ => myAudioSource.volume = musicSlider.value);
        myAudioSource.volume = musicSlider.value;
        myAudioSource.Play();
    }

    void PlayClip(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, sfxSlider.value);
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
