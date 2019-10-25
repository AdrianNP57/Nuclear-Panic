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
            if (tag == "LowRadiation")
            {
                fxPlayer.Play(fxPlayer.radiationLow);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().receiveingDamage = true;
            }
            else if (tag == "MediumRadiation")
            {
                fxPlayer.Play(fxPlayer.radiationMedium);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().receiveingDamage = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fxPlayer.Stop();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().receiveingDamage = false;
        }
    }
}