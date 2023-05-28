using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingBGManager : MonoBehaviour
{
    [SerializeField] Sprite endingWithNico;
    [SerializeField] Sprite endingWithoutNico;
    Image myImage;
    string PlayerPrefsNicoKey = "nico";

    void Awake()
    {
        myImage = GetComponent<Image>();
    }

    void Start()
    {
        myImage.sprite = PlayerPrefs.GetInt(PlayerPrefsNicoKey) == 1 ? endingWithNico : endingWithoutNico;
    }
}
