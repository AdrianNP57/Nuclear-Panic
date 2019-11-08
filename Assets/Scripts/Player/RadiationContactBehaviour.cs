using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationContactBehaviour : MonoBehaviour
{
    private int lowRadiationCount;
    private int mediumRadiationCount;

    private bool previouslyOnLow;
    private bool previouslyOnMedium;

    // Start is called before the first frame update
    void Awake()
    {
        lowRadiationCount = mediumRadiationCount = 0;
    }

    void Update() {
        if(previouslyOnLow != IsOnLowRadiation())
        {
            EventManager.TriggerEvent(previouslyOnLow? "LowRadiationExit" : "LowRadiationEnter");
        }

        if(previouslyOnMedium != IsOnMediumRadiation())
        {
            EventManager.TriggerEvent(previouslyOnMedium? "MediumRadiationExit" : "MediumRadiationEnter");
        }

        previouslyOnLow = IsOnLowRadiation();
        previouslyOnMedium = IsOnMediumRadiation();

        ShowLog();
    }

    public bool IsOnLowRadiation()
    {
        return lowRadiationCount > 0;
    }

    public bool IsOnMediumRadiation()
    {
        return mediumRadiationCount > 0;
    }

    private void ShowLog()
    {
        if(IsOnLowRadiation() && IsOnMediumRadiation())
        {
            DebugPanelBehaviour.Log("Radiation", "Low & Medium");
        }
        else if (IsOnLowRadiation())
        {
            DebugPanelBehaviour.Log("Radiation", "Low");
        }
        else if (IsOnMediumRadiation())
        {
            DebugPanelBehaviour.Log("Radiation", "Medium");
        } else
        {
            DebugPanelBehaviour.Log("Radiation", "None");
        }
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
}
