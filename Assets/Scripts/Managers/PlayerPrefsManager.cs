using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    string musicVolumePlayerPrefKey = "musicVolume";
    string sfxVolumePlayerPrefKey = "sfxVolume";

    void Start()
    {
        if (PlayerPrefs.HasKey(musicVolumePlayerPrefKey))
        {
            musicSlider.value = PlayerPrefs.GetFloat(musicVolumePlayerPrefKey);
        }
        if (PlayerPrefs.HasKey(sfxVolumePlayerPrefKey))
        {
            sfxSlider.value = PlayerPrefs.GetFloat(sfxVolumePlayerPrefKey);
        }
        musicSlider.onValueChanged.AddListener(_ => PlayerPrefs.SetFloat(musicVolumePlayerPrefKey, musicSlider.value));
        sfxSlider.onValueChanged.AddListener(_ => PlayerPrefs.SetFloat(sfxVolumePlayerPrefKey, musicSlider.value));
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(musicVolumePlayerPrefKey, musicSlider.value);
        PlayerPrefs.SetFloat(sfxVolumePlayerPrefKey, musicSlider.value);
    }
}
