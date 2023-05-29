using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI actorName;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] Image avatarImage;
    [SerializeField] RectTransform dialogueBox;
    [SerializeField] DialogueSO[] dialogues;
    [SerializeField][Range(0.05f, 0.1f)] float openDialogueSpeed = 0.07f;
    [SerializeField][Range(0.05f, 0.1f)] float closeDialogueSpeed = 0.1f;
    [SerializeField] float tutorialChatSpeed = 5f;
    Vector3 grow;
    Vector3 shrink;
    PauseManager pauseManager;
    int dialogueIndex = 0;
    public static bool isTutorialing { get; private set; } = false;

    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            pauseManager = FindObjectOfType<PauseManager>();
        }
        else
        {
            pauseManager = null;
        }
    }

    void Start()
    {
        grow = new Vector3(openDialogueSpeed, openDialogueSpeed, openDialogueSpeed);
        shrink = new Vector3(closeDialogueSpeed, closeDialogueSpeed, closeDialogueSpeed);
        dialogueBox.transform.localScale = Vector3.zero;
        if (pauseManager != null)
        {
            if (PlayerPrefs.HasKey(PlayerPrefsManager.CONST_TUTORIAL_KEY) &&
                PlayerPrefs.GetInt(PlayerPrefsManager.CONST_TUTORIAL_KEY) == 0)
            {
                return ;
            }
            else
            {
                StartCoroutine(DialogueDuringGame());
            }
        }
    }

    public void StartNewDialogue(DialogueSO[] newDialogues)
    {
        if (PlayerPrefs.HasKey(PlayerPrefsManager.CONST_TUTORIAL_KEY) &&
            PlayerPrefs.GetInt(PlayerPrefsManager.CONST_TUTORIAL_KEY) == 0)
        {
            return ;
        }
        dialogues = newDialogues;
        StartCoroutine(DialogueDuringGame());
    }

    IEnumerator DialogueDuringGame()
    {
        dialogueIndex = 0;
        pauseManager.Pause();
        isTutorialing = true;
        for (int i = 0; i < dialogues.Length; i++)
        {
            CallNextDialogue();
            yield return new WaitForSecondsRealtime(tutorialChatSpeed);
            CloseDialogueBox();
            yield return new WaitForSecondsRealtime(0.3f);
        }
        isTutorialing = false;
        pauseManager.Resume();
    }

    public void CallNextDialogue()
    {
        if (dialogues[dialogueIndex] == null)
        {
            dialogueIndex++;
            return;
        }
        actorName.text = dialogues[dialogueIndex].ActorName;
        messageText.text = dialogues[dialogueIndex].Message;
        avatarImage.sprite = dialogues[dialogueIndex].Avatar;
        dialogueBox.gameObject.SetActive(true);
        StartCoroutine(OpenBoxCoroutine());
        dialogueIndex++;
    }

    IEnumerator OpenBoxCoroutine()
    {
        while (dialogueBox.localScale.x < 1)
        {
            dialogueBox.localScale += grow;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        dialogueBox.localScale = Vector3.one;
    }

    IEnumerator CloseBoxCoroutine()
    {
        while (dialogueBox.localScale.x > 0)
        {
            dialogueBox.localScale -= shrink;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        dialogueBox.localScale = Vector3.zero;
        dialogueBox.gameObject.SetActive(false);
    }

    public void CloseDialogueBox()
    {
        StartCoroutine(CloseBoxCoroutine());
    }
}
