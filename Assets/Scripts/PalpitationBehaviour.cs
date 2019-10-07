using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalpitationBehaviour : MonoBehaviour
{
    public float mOscillationMax;
    public int mType; //1 = alpha, 2 = beta, 3 = gamma
    public float mOscillationPerFrame;
    private float mOriginalScaleX;
    private bool mJustSwichValue = false;
    void Start()
    {
        mOriginalScaleX = gameObject.transform.localScale.x;
    }
    void Update()
    {
        if (mJustSwichValue || Mathf.Abs(gameObject.transform.localScale.x - mOriginalScaleX) < mOscillationMax)
        {
            Debug.Log("OSCILLATION");
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