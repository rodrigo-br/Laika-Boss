using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public event EventHandler<int> OnTimeLineReached; 
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float currentTime;
    [SerializeField] float[] timelines;
    int timelineIndex = 0;
    bool isPaused = false;

    void Start()
    {
        SetTimerText();
    }

    void Update()
    {
        if (!isPaused)
        {
            currentTime -= Time.deltaTime;
            CheckForTimelines();
            SetTimerText();
        }
    }

    void CheckForTimelines()
    {
        if (timelineIndex < timelines.Length && currentTime <= timelines[timelineIndex])
        {
            OnTimeLineReached?.Invoke(this, timelineIndex);
            timelineIndex++;
        }
    }

    void SetTimerText()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timerText.text = time.ToString(@"mm\:ss\:ff");
    }

    public void PauseTimer() => isPaused = true;
}
