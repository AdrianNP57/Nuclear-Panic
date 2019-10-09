using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RadiationBar : MonoBehaviour
{
    private RadiationEffect radiationEffect;
    private Image barImage;
    public bool gamma, alpha, beta, alphaOut, betaOut = false; //set true if collision happened

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        radiationEffect = new RadiationEffect();
    }

    private void Update()
    {
        radiationEffect.Update();
        barImage.fillAmount = radiationEffect.GetRadiation();

        if (radiationEffect.GetRadiation() >= 1) //Player has obained enough damage to die (bar is filled)
        {
            Debug.Log("GAMEOVER");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameObject.Destroy(GameObject.Find("Player"));
        }

        checkCollision();   
    }

    private void checkCollision() //detects collision
    {
        if (gamma) //gamma collision
        {
            radiationEffect.SetRadiationTick(100f);
            //Cannot exit gamma, since you die.
        }

        if (alpha) //alpha collision
        {
            radiationEffect.SetRadiationTick(0.6f);
            if (alphaOut) //alpha collision exit
            {
                alpha = false;
                alphaOut = false;
            }
        }

        if (beta) //beta collision
        {
            radiationEffect.SetRadiationTick(0.2f);
            if (betaOut) //beta collision exit
            {
                beta = false;
                betaOut = false;
            }
        }
    }
}


public class RadiationEffect    //Class for radiation logic
{
    public const int maxRadiation = 100;
    private float radiationAmount, radiationTick; //Total amount of radiation (0-100)% & radiation taken per tick (0-100)%

    public RadiationEffect() //Constructor (Starting values)
    {
        radiationAmount = 0f;
        radiationTick = 0f;
    }

    public void Update()
    {
        if (GameObject.Find("RadiationBar").GetComponent<RadiationBar>().alpha ||
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().beta ||
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().gamma) //Checks if player is still in radiation or not 
        {
            radiationAmount += radiationTick * Time.deltaTime; //Update total radiation amount
        }
    }

    public float GetRadiation() //to return fillAmount for the bar
    {
        return radiationAmount / maxRadiation; //returns a value between (0-1) 
    }

    public void SetRadiationTick(float tick)
    {
        radiationTick += tick;
    }
}
