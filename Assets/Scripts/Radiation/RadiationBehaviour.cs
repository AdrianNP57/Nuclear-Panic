using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationBehaviour : MonoBehaviour
{
    public AudioEffectPlayer fxPlayer;

    public float mOscillationMax;
    public int mType; //1 = alpha, 2 = beta, 3 = gamma
    public float mOscillationPerFrame;
    private float mOriginalScaleX;
    private bool mJustSwichValue = false;
    
    // Start
    void Start()
    {
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
            if(tag == "Alpha")
            {
                fxPlayer.Play(fxPlayer.radiationMedium);
            }
            else if(tag == "Beta")
            {
                fxPlayer.Play(fxPlayer.radiationFast);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fxPlayer.Stop();
        }
    }
}