using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageForPlatform : MonoBehaviour
{
    // Properties
    public Sprite defaultSprite;
    public PlatformSprite[] platformSprites;

    void Awake()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Image image = GetComponent<Image>();

        SetSprite(renderer, image, defaultSprite);

        foreach(PlatformSprite platformSprite in platformSprites)
        {
            if(Application.platform == platformSprite.platform)
            {
                SetSprite(renderer, image, platformSprite.sprite);
            }
        }
    }

    private void SetSprite(SpriteRenderer renderer, Image image, Sprite sprite)
    {
        if(renderer != null)
        {
            renderer.sprite = sprite;
        }
        else
        {
            image.sprite = sprite;
        }
    }
}

[Serializable]
public struct PlatformSprite
{
    public RuntimePlatform platform;
    public Sprite sprite;
}