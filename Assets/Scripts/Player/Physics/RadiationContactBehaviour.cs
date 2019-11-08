using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationContactBehaviour : MonoBehaviour
{
    private PlayerController playerController;

    private int lowRadiationCount;
    private int mediumRadiationCount;

    private bool previouslyInLow;
    private bool previouslyInMedium;

    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();

        EventManager.StartListening("PlayerDied", OnPlayerDied);
        EventManager.StartListening("GameRestart", Init);

        Init();
    }

    private void Init()
    {
        lowRadiationCount = mediumRadiationCount = 0;
    }

    void Update() {
        if(playerController.isAlive)
        {
            if (previouslyInLow != IsInLowRadiation())
            {
                EventManager.TriggerEvent(previouslyInLow ? "LowRadiationExit" : "LowRadiationEnter");
            }

            if (previouslyInMedium != IsInMediumRadiation())
            {
                EventManager.TriggerEvent(previouslyInMedium ? "MediumRadiationExit" : "MediumRadiationEnter");
            }

            previouslyInLow = IsInLowRadiation();
            previouslyInMedium = IsInMediumRadiation();
        }

        ShowLog();
    }

    public bool IsInLowRadiation()
    {
        return lowRadiationCount > 0;
    }

    public bool IsInMediumRadiation()
    {
        return mediumRadiationCount > 0;
    }

    private void ShowLog()
    {
        if(IsInLowRadiation() && IsInMediumRadiation())
        {
            DebugPanelBehaviour.Log("Radiation", "Low & Medium");
        }
        else if (IsInLowRadiation())
        {
            DebugPanelBehaviour.Log("Radiation", "Low");
        }
        else if (IsInMediumRadiation())
        {
            DebugPanelBehaviour.Log("Radiation", "Medium");
        } else
        {
            DebugPanelBehaviour.Log("Radiation", "None");
        }
    }

    private void OnPlayerDied()
    {
        if(previouslyInLow)
        {
            EventManager.TriggerEvent("LowRadiationExit");
        }

        if(previouslyInMedium)
        {
            EventManager.TriggerEvent("MediumRadiationExit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LowRadiation")) lowRadiationCount++;
        if (collision.gameObject.CompareTag("MediumRadiation")) mediumRadiationCount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LowRadiation")) lowRadiationCount--;
        if (collision.gameObject.CompareTag("MediumRadiation")) mediumRadiationCount--;
    }
}
