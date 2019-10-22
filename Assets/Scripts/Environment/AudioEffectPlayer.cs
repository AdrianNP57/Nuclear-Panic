using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectPlayer : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip land;
    public AudioClip radiationLow;
    public AudioClip radiationMedium;

    public AudioSource fxSource;

    private bool playingRadiationLow;
    private bool playingRadiationMedium;


    public void Play(AudioClip clip)
    {

        if(clip == radiationLow && !playingRadiationLow)
        {
            fxSource.PlayOneShot(clip);
            playingRadiationLow = true;
        }
        else if(clip == radiationMedium && !playingRadiationMedium)
        {
            fxSource.PlayOneShot(clip);
            playingRadiationMedium = true;
        }
        else if(clip != radiationLow && clip != radiationMedium)
        {
            fxSource.PlayOneShot(clip);
        }
    }

    public void Stop()
    {
        fxSource.Stop();
        playingRadiationLow = playingRadiationMedium = false;
    }
}
