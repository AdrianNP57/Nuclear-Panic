using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationOscillationBehaviour : MonoBehaviour
{
    public float mOscillationMax;
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
}