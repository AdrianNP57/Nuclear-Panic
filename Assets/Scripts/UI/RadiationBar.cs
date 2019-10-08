using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RadiationBar : MonoBehaviour
{
    private RadiationEffect radiationEffect;
    private Image barImage;
    public bool gamma, alpha, beta = false; //set true if collision happened

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        radiationEffect = new RadiationEffect();
    }

    private void Update()
    {
        radiationEffect.Update();
        barImage.fillAmount = radiationEffect.GetRadiation();

        if (gamma) //gamma collision
        {
            gamma = false;
            radiationEffect.SetRadiationTick(100f);
        }

        if (alpha) //alpha collision
        {
            alpha = false;
            radiationEffect.SetRadiationTick(8f);
        }

        if (beta) //beta collision
        {
            beta = false;
            radiationEffect.SetRadiationTick(2f);
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
        radiationAmount += radiationTick * Time.deltaTime; //Update total radiation amount

        if(radiationAmount >= 100) //Player has obained enough damage to die
        {
            Debug.Log("GAMEOVER");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameObject.Destroy(GameObject.Find("Player"));
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
