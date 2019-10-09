using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GlassBehaviour : MonoBehaviour
{
    public GameObject thugGlasses;
    public GameObject tileMap;
    public Material darkMaterial;
    public Camera mainCamera;
    public GameObject haloLight;
    private GameObject backgroundsPrefab;
    Material defaultMaterial;

    bool glassesOn;
    Camera changeColor;
    // Start is called before the first frame update
    void Start()
    {
        glassesOn = true;
        changeColor = mainCamera.GetComponent<Camera>();
        defaultMaterial = GameObject.Find("Tilemap").GetComponent<TilemapRenderer>().material;
        backgroundsPrefab = GameObject.Find("Background");
        backgroundsPrefab.SetActive(false);         // this is only here because as of right now when we start the level the glasses are on automatically
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Glasses") && !glassesOn)
        {
            Debug.Log("Glasses on");
            glassesOn = true;
            backgroundsPrefab.SetActive(false);
        }
        else if (Input.GetButtonDown("Glasses") && glassesOn)
        {
            Debug.Log("Glasses off");
            glassesOn = false;
            backgroundsPrefab.SetActive(true);
        }

        Material targetMaterial = glassesOn? darkMaterial : defaultMaterial;
        Color backgroundColor = glassesOn? new Color(0.0f, 0.0f, 0.0f) : new Color(0.2f, 0.3f, 0.5f);

        thugGlasses.SetActive(glassesOn);
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
