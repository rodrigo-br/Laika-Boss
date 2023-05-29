using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Toggle tutorial;
    [SerializeField] Toggle nicosHelp;
    Image tutorialImage;
    Image nicosHelpImage;
    Color toggleColor = new Color(0.5f, 0, 1f);
    public const string CONST_MUSIC_KEY = "musicVolume";
    public const string CONST_SFX_KEY = "sfxVolume";
    public const string CONST_TUTORIAL_KEY = "tutorial";
    public const string CONST_NICOHELP_KEY = "nicosHelp";
    public const string CONST_NICO_KEY = "nico";

    void Awake()
    {
        tutorialImage = tutorial.GetComponentInChildren<Image>();
        nicosHelpImage = nicosHelp.GetComponentInChildren<Image>();
    }

    void Start()
    {
        if (PlayerPrefs.HasKey(CONST_MUSIC_KEY))
        {
            musicSlider.value = PlayerPrefs.GetFloat(CONST_MUSIC_KEY);
        }
        if (PlayerPrefs.HasKey(CONST_SFX_KEY))
        {
            sfxSlider.value = PlayerPrefs.GetFloat(CONST_SFX_KEY);
        }
        if (PlayerPrefs.HasKey(CONST_TUTORIAL_KEY))
        {
            tutorial.isOn = PlayerPrefs.GetInt(CONST_TUTORIAL_KEY) == 1 ? true : false;
        }
        if (PlayerPrefs.HasKey(CONST_NICOHELP_KEY))
        {
            nicosHelp.isOn = PlayerPrefs.GetInt(CONST_NICOHELP_KEY) == 1 ? true : false;
        }
        tutorialImage.color = tutorial.isOn == true ? toggleColor : Color.gray;
        nicosHelpImage.color = nicosHelp.isOn == true ? toggleColor : Color.gray;
        musicSlider.onValueChanged.AddListener(_ => PlayerPrefs.SetFloat(CONST_MUSIC_KEY, musicSlider.value));
        sfxSlider.onValueChanged.AddListener(_ => PlayerPrefs.SetFloat(CONST_SFX_KEY, sfxSlider.value));
        tutorial.onValueChanged.AddListener(_ => PlayerPrefs.SetInt(CONST_TUTORIAL_KEY, tutorial.isOn == true ? 1 : 0));
        nicosHelp.onValueChanged.AddListener(_ => PlayerPrefs.SetInt(CONST_NICOHELP_KEY, nicosHelp.isOn == true ? 1 : 0));
        tutorial.onValueChanged.AddListener(_ => tutorialImage.color = tutorial.isOn == true ? toggleColor : Color.gray);
        nicosHelp.onValueChanged.AddListener(_ => nicosHelpImage.color = nicosHelp.isOn == true ? toggleColor : Color.gray);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(CONST_MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(CONST_SFX_KEY, sfxSlider.value);
        PlayerPrefs.SetInt(CONST_TUTORIAL_KEY, tutorial.isOn == true ? 1 : 0);
        PlayerPrefs.SetInt(CONST_NICOHELP_KEY, nicosHelp.isOn == true ? 1 : 0);
    }
}
