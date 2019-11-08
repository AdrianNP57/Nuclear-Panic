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

    private PlayerController playerController;

    private float currentRadiationAmount;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        EventManager.StartListening("GameRestart", Init);

        Init();
    }

    private void Init()
    {
        currentRadiationAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float radiationIncrease = contact.IsInLowRadiation() ? lowRadiationDamage : 0;
        radiationIncrease = contact.IsInMediumRadiation() ? mediumRadiationDamage : radiationIncrease;

        currentRadiationAmount += radiationIncrease * Time.deltaTime;
        currentRadiationAmount = currentRadiationAmount < 1 ? currentRadiationAmount : 1;

        radiationBar.fillAmount = currentRadiationAmount;
        radiationText.text = String.Format("{0:0.00}", currentRadiationAmount * 10);

        if(currentRadiationAmount >= 1 && playerController.isAlive)
        {
            EventManager.TriggerEvent("PlayerDied");
        }
    }
}
