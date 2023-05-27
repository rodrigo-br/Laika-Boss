using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] Texture2D[] cursorTextureArray;
    [SerializeField] float frameRate;
    int frameCount;
    int currentFrame;
    Vector2 hotspot;

    void Start()
    {
        frameCount = cursorTextureArray.Length;
        hotspot = new Vector2(cursorTextureArray[0].width / 2, cursorTextureArray[0].height / 2);
        Cursor.SetCursor(cursorTextureArray[0], hotspot, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Confined;
        StartCoroutine(AnimeCursorCoroutine());
    }

    IEnumerator AnimeCursorCoroutine()
    {
        while(true)
        {
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(cursorTextureArray[currentFrame], hotspot, CursorMode.Auto);
            yield return new WaitForSecondsRealtime(frameRate);
        }
    }
}
