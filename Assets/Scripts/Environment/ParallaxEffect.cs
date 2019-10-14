using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, currentPos;
    public GameObject cam;
    public float parallaxEffect;
    private float dist;
    private float temp;

    private float initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position.x;
        Init();
    }

    public void Init()
    {
        currentPos = initialPos - 10;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        temp = 0;
        dist = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        temp = (cam.transform.position.x * (1.0f - parallaxEffect));
        dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(currentPos + dist, transform.position.y, transform.position.z);
        if(temp > currentPos + length)
        {
            currentPos += length;
        }
        else if(temp < currentPos - length)
        {
            currentPos -= length;
        }
    }
}
