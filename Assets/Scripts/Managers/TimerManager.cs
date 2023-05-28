using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float currentTime;

    void Start()
    {
        SetTimerText();
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            timerText.color = Color.green;
            timerText.text = "SUCCESS!";
            gameObject.SetActive(false);
            SceneManager.LoadScene(3);
            return ;
        }
        SetTimerText();
    }

    void SetTimerText()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timerText.text = time.ToString(@"mm\:ss\:ff");
    }
}