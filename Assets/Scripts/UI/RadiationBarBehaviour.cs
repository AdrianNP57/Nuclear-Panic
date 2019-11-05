using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadiationBarBehaviour : MonoBehaviour
{
    public float lowRadiationDamage;
    public float mediumRadiationDamage;

    public TextMeshProUGUI radiationText;
    public Image radiationBar;
    public RadiationContactBehaviour contact;

    private float currentRadiationAmount;

    // Update is called once per frame
    void Update()
    {
        float radiationIncrease = contact.IsOnLowRadiation() ? lowRadiationDamage : 0;
        radiationIncrease = contact.IsOnMediumRadiation() ? mediumRadiationDamage : radiationIncrease;

        currentRadiationAmount += radiationIncrease * Time.deltaTime;
        currentRadiationAmount = currentRadiationAmount < 1 ? currentRadiationAmount : 1;

        radiationBar.fillAmount = currentRadiationAmount;
        radiationText.text = String.Format("{0:0.00}", currentRadiationAmount * 10);

        if(currentRadiationAmount >= 1)
        {
            EventManager.TriggerEvent("PlayerDied");
        }
    }
}
