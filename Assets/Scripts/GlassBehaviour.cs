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
    Material defaultMaterial;

    int check;
    Camera changeColor;
    // Start is called before the first frame update
    void Start()
    {
        check = 0;
        changeColor = mainCamera.GetComponent<Camera>();
        defaultMaterial = GameObject.Find("Tilemap").GetComponent<TilemapRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && (check == 0))
        {
            Debug.Log("Glasses on");
            check = 1;

            // for line of vision thingy
            changeColor.backgroundColor = new Color(0.0f, 0.0f, 0.0f);
            tileMap.GetComponent<TilemapRenderer>().material = darkMaterial;
            thugGlasses.SetActive(true);
            haloLight.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.G) && (check == 1))
        {
            Debug.Log("Glasses off");
            check = 0;
            changeColor.backgroundColor = Color.blue/*new Color(49.0f, 77.0f, 121.0f)*/;
            thugGlasses.SetActive(false);
            haloLight.SetActive(false);
            tileMap.GetComponent<TilemapRenderer>().material = defaultMaterial;
        }
        else { }

    }
}
