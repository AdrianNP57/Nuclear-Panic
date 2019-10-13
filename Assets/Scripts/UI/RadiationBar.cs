using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RadiationBar : MonoBehaviour
{
    public GameObject gameOverPanel;

    private RadiationEffect radiationEffect;
    private Image barImage;
    public bool lethalRadiation, lowRadiation, mediumRadiation, lowRadiationOut, mediumRadiationOut = false; //set true if collision happened

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
            GameObject.FindGameObjectWithTag("Player")?.SetActive(false);
            gameOverPanel.SetActive(true);

            if(Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        checkCollision();   
    }

    private void checkCollision() //detects collision
    {
        if (lethalRadiation) //lethal collision
        {
            radiationEffect.SetRadiationTick(100f);
            //Cannot exit lethal, since you die.
        }

        if (lowRadiation) //low collision
        {
            radiationEffect.SetRadiationTick(0.6f);
            if (lowRadiationOut) //low collision exit
            {
                lowRadiation = false;
                lowRadiationOut = false;
            }
        }

        if (mediumRadiation) //medium collision
        {
            radiationEffect.SetRadiationTick(0.2f);
            if (mediumRadiationOut) //medium collision exit
            {
                mediumRadiation = false;
                mediumRadiationOut = false;
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
        if (GameObject.Find("RadiationBar").GetComponent<RadiationBar>().lowRadiation ||
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().mediumRadiation ||
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().lethalRadiation) //Checks if player is still in radiation or not 
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
