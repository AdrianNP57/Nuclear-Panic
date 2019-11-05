using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationContactBehaviour : MonoBehaviour
{
    private int lowRadiationCount;
    private int mediumRadiationCount;

    // Start is called before the first frame update
    void Awake()
    {
        lowRadiationCount = mediumRadiationCount = 0;
    }

    public bool IsOnLowRadiation()
    {
        return lowRadiationCount > 0;
    }

    public bool IsOnMediumRadiation()
    {
        return mediumRadiationCount > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LowRadiation")) lowRadiationCount++;
        if (collision.gameObject.CompareTag("MediumRadiation")) mediumRadiationCount++;

        if(lowRadiationCount > 0 || mediumRadiationCount > 0)
        {
            EventManager.TriggerEvent("DamageStart");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LowRadiation")) lowRadiationCount--;
        if (collision.gameObject.CompareTag("MediumRadiation")) mediumRadiationCount--;

        if (lowRadiationCount <= 0 || mediumRadiationCount <= 0)
        {
            EventManager.TriggerEvent("DamageEnd");
        }
    }

    void Update() { }
}
