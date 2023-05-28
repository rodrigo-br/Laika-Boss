using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Toggle tutorial;
    Image toggleImage;
    Color toggleColor = new Color(0.5f, 0, 1f);
    string musicVolumePlayerPrefKey = "musicVolume";
    string sfxVolumePlayerPrefKey = "sfxVolume";
    string tutorialPlayerPrefKey = "tutorial";

    void Awake()
    {
        toggleImage = tutorial.GetComponentInChildren<Image>();
    }

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
        if (PlayerPrefs.HasKey(tutorialPlayerPrefKey))
        {
            tutorial.isOn = PlayerPrefs.GetInt(tutorialPlayerPrefKey) == 1 ? true : false;
        }
        musicSlider.onValueChanged.AddListener(_ => PlayerPrefs.SetFloat(musicVolumePlayerPrefKey, musicSlider.value));
        sfxSlider.onValueChanged.AddListener(_ => PlayerPrefs.SetFloat(sfxVolumePlayerPrefKey, sfxSlider.value));
        tutorial.onValueChanged.AddListener(_ => PlayerPrefs.SetInt(tutorialPlayerPrefKey, tutorial.isOn == true ? 1 : 0));
        tutorial.onValueChanged.AddListener(_ => toggleImage.color = tutorial.isOn == true ? toggleColor : Color.gray);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(musicVolumePlayerPrefKey, musicSlider.value);
        PlayerPrefs.SetFloat(sfxVolumePlayerPrefKey, sfxSlider.value);
        PlayerPrefs.SetInt(tutorialPlayerPrefKey, tutorial.isOn == true ? 1 : 0);
    }
}
