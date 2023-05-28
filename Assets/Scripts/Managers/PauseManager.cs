using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; } = false;
    PauseManagerAction action;
    [SerializeField] GameObject menu;

    void Awake()
    {
        action = new PauseManagerAction();
        menu.GetComponentInChildren<Button>().onClick.AddListener(SetPauseState);
    }

    void OnEnable()
    {
        action.Enable();
    }

    void Start()
    {
        action.Pause.PauseGame.performed += _ => SetPauseState();
    }

    void SetPauseState()
    {
        if (DialogueManager.isTutorialing) { return; }

        if (IsPaused)
        {
            Resume();
        }
        else
        {
            Pause(true);
        }
    }

    void OnDisable()
    {
        action.Disable();
    }

    public void Pause(bool activeMenu = false)
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        Debug.Log(SceneManager.sceneCountInBuildSettings - 2);
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 2)
        {
            Time.timeScale = 0;
            IsPaused = true;
        }
        menu.SetActive(activeMenu);
    }

    public void Resume()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 2)
        {
            Time.timeScale = 1;
            IsPaused = false;
        }
        menu.SetActive(false);
    }
}
