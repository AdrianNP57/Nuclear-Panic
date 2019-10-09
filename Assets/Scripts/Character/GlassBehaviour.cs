using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GlassBehaviour : MonoBehaviour
{
    public GameObject tileMap;
    public Material darkMaterial;
    public GameObject haloLight;

    public SpriteRenderer glassesRenderer;
    public Sprite glassesOnSprite;
    public Sprite glassesOffSprite;

    Material defaultMaterial;

    bool glassesOn;
    Camera changeColor;
    // Start is called before the first frame update
    void Start()
    {
        glassesOn = true;
        changeColor = Camera.main.GetComponent<Camera>();
        defaultMaterial = GameObject.Find("Tilemap").GetComponent<TilemapRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Glasses") && !glassesOn)
        {
            Debug.Log("Glasses on");
            glassesOn = true;
        }
        else if (Input.GetButtonDown("Glasses") && glassesOn)
        {
            Debug.Log("Glasses off");
            glassesOn = false;
        }

        Material targetMaterial = glassesOn? darkMaterial : defaultMaterial;
        Color backgroundColor = glassesOn? new Color(0.0f, 0.0f, 0.0f) : new Color(0.2f, 0.3f, 0.5f);

        glassesRenderer.sprite = glassesOn ? glassesOnSprite : glassesOffSprite;
        haloLight.SetActive(glassesOn);
        changeColor.backgroundColor = backgroundColor;
        tileMap.GetComponent<TilemapRenderer>().material = targetMaterial;
        foreach (GameObject level in GetComponent<PlayerBehaviour>().mBufferLevels)
        {

            level.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TilemapRenderer>().material = targetMaterial;
        }

        if(glassesOn)
        {
            ShowRadiation();
        }
        else
        {
            HideRadiation();
        }
    }

    private void ShowRadiation()
    {
        Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Radiation");
    }

    // Turn off the bit using an AND operation with the complement of the shifted int:
    private void HideRadiation()
    {
        Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Radiation"));
    }
}
