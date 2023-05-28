using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageSelector : MonoBehaviour
{
    [SerializeField] Sprite[] images;
    [SerializeField] float timeBetweenImageChange = 3f;
    [SerializeField] DialogueManager dialogueManager;
    float currentTimeLeft;
    int index = 0;
    Image myImage;

    void Awake()
    {
        myImage = GetComponent<Image>();
    }

    void Start()
    {
        myImage.sprite = images[0];
        dialogueManager.Invoke("CallNextDialogue", 1f);
        currentTimeLeft = timeBetweenImageChange;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            currentTimeLeft = 0;
        }
        if (currentTimeLeft <= 0)
        {
            index++;
            if (index == images.Length)
            {
                SceneManager.LoadScene(1);
                return ;
            }
            currentTimeLeft = timeBetweenImageChange;
            myImage.sprite = images[index];
            dialogueManager.CloseDialogueBox();
            dialogueManager.Invoke("CallNextDialogue", 1f);
        }
        else
        {
            currentTimeLeft -= Time.deltaTime;
        }

    }
}
