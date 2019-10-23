using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, lengthY, currentPos, currentPosY;
    public GameObject cam;
    public float parallaxEffect;
    private float dist, distY;
    private float temp, tempY;

    private float initialPos, initialPosY;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position.x;
        initialPosY = transform.position.y;
        Init();
    }

    public void Init()
    {
        currentPos = initialPos - 10;
        currentPosY = initialPosY - 1;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;

        temp = tempY = 0;
        dist = distY = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        temp = (cam.transform.position.x * (1.0f - parallaxEffect));
        tempY = (cam.transform.position.y * (1.0f - parallaxEffect));

        dist = (cam.transform.position.x * parallaxEffect);
        distY = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(currentPos + dist, currentPosY + distY, transform.position.z);

        if(temp > currentPos + length)
        {
            currentPos += length;
        }
        else if(temp < currentPos - length)
        {
            currentPos -= length;
        }

        if (tempY > currentPosY + lengthY)
        {
            currentPosY += lengthY;
        }
        else if (tempY < currentPosY - lengthY)
        {
            currentPosY -= lengthY;
        }
    }
}
