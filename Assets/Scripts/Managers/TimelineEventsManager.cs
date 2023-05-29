using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimelineEventsManager : MonoBehaviour
{
    [SerializeField] GameObject nicoShip;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] DialogueSO[] dialogues1;
    [SerializeField] DialogueSO[] dialogues2;
    [SerializeField] DialogueSO[] dialogues3;
    [SerializeField] DialogueManager dialogueManager;
    System.Action[] timelineActions;
    TimerManager timerManager;

    void Awake()
    {
        timerManager = GetComponent<TimerManager>();
        timelineActions = new System.Action[] {
            LaikaCalling_1,
            NicoArriving,
            LoadVictoryScene,
        };
    }

    void OnEnable()
    {
        timerManager.OnTimeLineReached += TimerManager_OnTimeLineReached;
    }

    void OnDisable()
    {
        timerManager.OnTimeLineReached -= TimerManager_OnTimeLineReached;
    }

    void TimerManager_OnTimeLineReached(object sender, int index)
    {
        if (index < timelineActions.Length)
        {
            timelineActions[index]();
        }
    }

    void LaikaCalling_1()
    {
        dialogueManager.StartNewDialogue(dialogues1);
    }

    void NicoArriving()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsManager.CONST_NICOHELP_KEY) == 0)
        {
            return ;
        }
        nicoShip.SetActive(true);
        dialogueManager.StartNewDialogue(dialogues2);
        PlayerPrefs.SetInt(PlayerPrefsManager.CONST_NICO_KEY, 1);
    }

    void LoadVictoryScene()
    {
        timerManager.PauseTimer();
        timerText.color = Color.green;
        timerText.text = "SUCCESS!";
        dialogueManager.StartNewDialogue(dialogues3);
        Invoke("LoadScene", 0.2f);
    }

    void LoadScene() => SceneManager.LoadScene(3);
}
