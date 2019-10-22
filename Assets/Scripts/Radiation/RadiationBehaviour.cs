using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationBehaviour : MonoBehaviour
{
    public float mOscillationMax;
    public float mOscillationPerFrame;

    private AudioEffectPlayer fxPlayer;
    private float mOriginalScaleX;
    private bool mJustSwichValue = false;

    private bool currentlyPlayingLow = false;
    private bool currentlyPlayingMedium = false;
    
    // Start
    void Start()
    {
        fxPlayer = Camera.main.GetComponent<AudioEffectPlayer>();
        mOriginalScaleX = gameObject.transform.localScale.x;
    }

    // Update
    void Update()
    {
        if (mJustSwichValue || Mathf.Abs(gameObject.transform.localScale.x - mOriginalScaleX) < mOscillationMax)
        {
            //Debug.Log("OSCILLATION");
            Vector3 currScale = gameObject.transform.localScale;
            currScale.x += mOscillationPerFrame;
            currScale.y += mOscillationPerFrame;
            gameObject.transform.localScale = currScale;
            mJustSwichValue = false;
        }
        else
        {
            mOscillationPerFrame = -mOscillationPerFrame;
            mJustSwichValue = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(tag == "LowRadiation" && !currentlyPlayingLow)
            {
                fxPlayer.Play(fxPlayer.radiationLow);
                currentlyPlayingLow = true;
            }
            else if(tag == "MediumRadiation" && !currentlyPlayingMedium)
            {
                fxPlayer.Play(fxPlayer.radiationMedium);
                currentlyPlayingMedium = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fxPlayer.Stop();
            currentlyPlayingLow = currentlyPlayingMedium = false;
        }
    }
}