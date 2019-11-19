using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageForPlatform : MonoBehaviour
{
    // Properties
    public Sprite defaultSprite;
    public PlatformSprite[] platformSprites;

    void Awake()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = defaultSprite;

        foreach(PlatformSprite platformSprite in platformSprites)
        {
            if(Application.platform == platformSprite.platform)
            {
                renderer.sprite = platformSprite.sprite;
            }
        }
    }
}

[Serializable]
public struct PlatformSprite
{
    public RuntimePlatform platform;
    public Sprite sprite;
}