using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GlassBehaviour : MonoBehaviour
{
    public Light glassesLight;
    public Light ambientLight;

    private bool glassesOn;

    // Start is called before the first frame update
    void Awake()
    {
        EventManager.StartListening("EasyDifficultyChosen", SetEasyMode);
        EventManager.StartListening("HardDifficultyChosen", SetHardMode);

        Init();
    }

    public void Init()
    {
        glassesOn = false;
        HideRadiation();
    }

    private void SwapGlasses()
    {
        glassesOn = !glassesOn;

        glassesLight.gameObject.SetActive(glassesOn);
        ambientLight.gameObject.SetActive(!glassesOn);

        EventManager.TriggerEvent(glassesOn? "GlassesOn" : "GlassesOff");

        if(glassesOn)
        {
            ShowRadiation();
        } else
        {
            HideRadiation();
        }
    }

    private void SetDifficulty(bool easy)
    {
        glassesLight.intensity = easy ? 4 : 10;
        glassesLight.range = easy ? 235 : 7;

        if(!easy)
        {
            EventManager.StartListening("InputGlasses", SwapGlasses);
        } else
        {
            SwapGlasses();
        }
    }

    private void SetEasyMode()
    {
        SetDifficulty(true);
    }

    private void SetHardMode()
    {
        SetDifficulty(false);
    }

    private void ShowRadiation()
    {
        Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Radiation");
    }

    private void HideRadiation()
    {
        Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Radiation"));
    }

    private void Update()
    {
        
    }
}
