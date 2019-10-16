using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class RadiationBar : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject restartText;

    private RadiationEffect radiationEffect;
    private Image barImage;
    public bool lethalRadiation, lowRadiation, mediumRadiation, lowRadiationOut, mediumRadiationOut; //set true if collision happened

    public GameObject player;
    public GameObject background;
    public AudioSource musicSource;
    public TextMeshProUGUI radiationDigit;

    private bool canRestart;
    private bool playerDead;

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        radiationEffect = new RadiationEffect();

        Init();
    }

    private void Init()
    {
        lethalRadiation = lowRadiation = mediumRadiation = lowRadiationOut = mediumRadiationOut = false;
    }

    private void Update()
    {
        radiationEffect.Update();
        barImage.fillAmount = radiationEffect.GetRadiation();

        if (radiationEffect.GetRadiation() > 1) //if -> updates digit of radiation bar
        {
            radiationDigit.text = String.Format("{0:0.00}", 10.00);
        }
        else if (radiationEffect.GetRadiation() == 0) 
        {
            radiationDigit.text = String.Format("{0:0.00}", 0.00);
        }
        else
        {
            radiationDigit.text = String.Format("{0:0.0#}", radiationEffect.GetRadiation() * 10f);
        }

        if (radiationEffect.GetRadiation() >= 1) //Player has obained enough damage to die (bar is filled)
        {
            ChangeGameState(false);

            if(Input.GetButtonDown("Jump") && canRestart)
            {
                RestartScene();
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

    private void RestartScene()
    {
        ChangeGameState(true);
        Init();

        player.GetComponent<PlayerBehaviour>().Init();
        GameObject.Find("LevelPool").GetComponent<LevelPoolManager>().ReInit();
        Camera.main.GetComponent<CameraFollowingPlayer>().Init();
        radiationEffect.Init();

        foreach(Transform parallaxItem in background.transform)
        {
            parallaxItem.gameObject.GetComponent<ParallaxEffect>().Init();
        }

        musicSource.Stop();
        musicSource.Play();
    }

    private void ChangeGameState(bool activeGame)
    {
        if (!activeGame && !playerDead)
        {
            player.GetComponent<PlayerBehaviour>().Die();
            StartCoroutine(PreventImmediateGameRestart());
        }

        gameOverPanel.SetActive(!activeGame);
        playerDead = !activeGame;
    }

    private IEnumerator PreventImmediateGameRestart()
    {
        canRestart = false;
        restartText.SetActive(false);

        yield return new WaitForSeconds(2.0f);

        canRestart = true;
        restartText.SetActive(true);
    }
}

public class RadiationEffect    //Class for radiation logic
{
    public const int maxRadiation = 100;
    private float radiationAmount, radiationTick; //Total amount of radiation (0-100)% & radiation taken per tick (0-100)%

    public RadiationEffect() //Constructor (Starting values)
    {
        Init();
    }

    public void Init()
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
