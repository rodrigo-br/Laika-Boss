using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] Button scene0Button;
    [SerializeField] Button scene1Button;

    void Start()
    {
        if (scene0Button != null)
        {
            scene0Button.onClick.AddListener(() => SceneManager.LoadScene(0));
        }
        if (scene1Button != null)
        {
            scene1Button.onClick.AddListener(() => SceneManager.LoadScene(1));
        }
    }
}
