using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectPlayer : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip land;
    public AudioClip radiationMedium;
    public AudioClip radiationFast;

    public AudioSource fxSource;

    // Start is called before the first frame update
    void Start()
    {
        //source = GetComponent<AudioSource>(); 
    }

    public void Play(AudioClip clip)
    {
        fxSource.PlayOneShot(clip);
    }

    public void Stop()
    {
        fxSource.Stop();
    }
}
