using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
    Vector2 offset;
    Material material;

    void Awake()
    {
        Image image = GetComponent<Image>();
        image.material = Instantiate(image.material);
        material = image.material;
    }
        

    void FixedUpdate() => material.mainTextureOffset += (moveSpeed * Time.deltaTime);
}
