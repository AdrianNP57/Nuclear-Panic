using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadiationBar : MonoBehaviour
{
    private RadiationEffect radiationEffect;
    private Image barImage;

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        radiationEffect = new RadiationEffect();
    }

    private void Update()
    {
        radiationEffect.Update();
        barImage.fillAmount = radiationEffect.GetRadiation();
    }
}


public class RadiationEffect    //Class for radiation logic
{
    public const int maxRadiation = 100;
    private float radiationAmount, radiationIncrease;

    public RadiationEffect() //Constructor
    {
        radiationAmount = 0;
        radiationIncrease = 5f;
    }

    public void Update()
    {
        radiationAmount += radiationIncrease * Time.deltaTime;
    }

    public float GetRadiation() //to return fillAmount
    {
        return radiationAmount / maxRadiation; //returns a value between (0-1) 
    }
}
