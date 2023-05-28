using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        Time.timeScale = 0;
        IsPaused = true;
        menu.SetActive(activeMenu);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        IsPaused = false;
        menu.SetActive(false);
    }
}
