using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingBGManager : MonoBehaviour
{
    [SerializeField] Sprite endingWithNico;
    [SerializeField] Sprite endingWithoutNico;
    Image myImage;

    void Awake()
    {
        myImage = GetComponent<Image>();
    }

    void Start()
    {
        myImage.sprite = PlayerPrefs.GetInt(PlayerPrefsManager.CONST_NICO_KEY) == 1 ? endingWithNico : endingWithoutNico;
    }
}
