using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaAnchorBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float screenHeight = Camera.main.pixelHeight;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, screenHeight / 2, 0));
    }
}
