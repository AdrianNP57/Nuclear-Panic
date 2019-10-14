using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GlassBehaviour : MonoBehaviour
{
    public GameObject tileMap;
    public GameObject firstBackground;
    public GameObject firstBackgroundpartOne;
    public GameObject secondBackground;
    public GameObject secondBackgroundpartOne;
    public GameObject thirdBackground;
    public GameObject thirdBackgroundpartOne;
    public GameObject thirdBackgroundpartTwo;
    public GameObject fourthBackground;
    public GameObject fourthBackgroundpartOne;
    public GameObject fourthBackgroundpartTwo;
    public Material darkMaterial;
    public GameObject haloLight;
    private GameObject backgroundPrefab;

    public GameObject levelPool;
    public SpriteRenderer glassesRenderer;
    public Sprite glassesOnSprite;
    public Sprite glassesOffSprite;

    Material defaultMaterial;
    Material defaultMaterialBackground1;
    Material defaultMaterialBackground2;
    Material defaultMaterialBackground3;
    Material defaultMaterialBackground4;

    bool glassesOn;
    Camera changeColor;
    // Start is called before the first frame update
    void Start()
    {
        glassesOn = true;
        changeColor = Camera.main.GetComponent<Camera>();
        defaultMaterial = GameObject.Find("Tilemap").GetComponent<TilemapRenderer>().material;
        defaultMaterialBackground1 = GameObject.Find("4").GetComponent<SpriteRenderer>().material;
        defaultMaterialBackground2 = GameObject.Find("3").GetComponent<SpriteRenderer>().material;
        defaultMaterialBackground3 = GameObject.Find("2").GetComponent<SpriteRenderer>().material;
        defaultMaterialBackground4 = GameObject.Find("1").GetComponent<SpriteRenderer>().material;
        backgroundPrefab = GameObject.Find("Background");
        //backgroundPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Glasses") && !glassesOn)
        {
            Debug.Log("Glasses on");
            glassesOn = true;
            //backgroundPrefab.SetActive(false);
        }
        else if (Input.GetButtonDown("Glasses") && glassesOn)
        {
            Debug.Log("Glasses off");
            glassesOn = false;
            //backgroundPrefab.SetActive(true);
        }

        Material targetMaterial = glassesOn? darkMaterial : defaultMaterial;
        Color backgroundColor = glassesOn? new Color(0.0f, 0.0f, 0.0f) : new Color(0.2f, 0.3f, 0.5f);

        Material targetMaterial1 = glassesOn ? darkMaterial : defaultMaterial;
        //Color backgroundColor1 = glassesOn ? new Color(0.0f, 0.0f, 0.0f) : new Color(0.2f, 0.3f, 0.5f);

        glassesRenderer.sprite = glassesOn ? glassesOnSprite : glassesOffSprite;
        haloLight.SetActive(glassesOn);
        changeColor.backgroundColor = backgroundColor;
        tileMap.GetComponent<TilemapRenderer>().material = targetMaterial;
        firstBackground.GetComponent<SpriteRenderer>().material = targetMaterial1;
        firstBackgroundpartOne.GetComponent<SpriteRenderer>().material = targetMaterial1;
        secondBackground.GetComponent<SpriteRenderer>().material = targetMaterial1;
        secondBackgroundpartOne.GetComponent<SpriteRenderer>().material = targetMaterial1;
        thirdBackground.GetComponent<SpriteRenderer>().material = targetMaterial1;
        thirdBackgroundpartOne.GetComponent<SpriteRenderer>().material = targetMaterial1;
        thirdBackgroundpartTwo.GetComponent<SpriteRenderer>().material = targetMaterial1;
        fourthBackground.GetComponent<SpriteRenderer>().material = targetMaterial1;
        fourthBackgroundpartOne.GetComponent<SpriteRenderer>().material = targetMaterial1;
        fourthBackgroundpartTwo.GetComponent<SpriteRenderer>().material = targetMaterial1;

        foreach (GameObject level in levelPool.GetComponent<LevelPoolManager>().levels)
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
