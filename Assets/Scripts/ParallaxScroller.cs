using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Since we are using Image instead of SpriteRenderer, the material 
    is changed along the variable values, so we need to create a new
    material for each object with this script
*/
public class ParallaxScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
    Vector2 offset;
    Material material;

    void Awake()
    {
        material = GetComponent<Image>().material;
    }

    void FixedUpdate()
    {
        material.mainTextureOffset += (moveSpeed * Time.deltaTime);
    }
}
