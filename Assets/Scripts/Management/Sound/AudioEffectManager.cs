using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO test what happens when dying in radiation
public class AudioEffectManager : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip land;
    public AudioClip radiationLow;
    public AudioClip radiationMedium;

    public AudioSource fxSource;
    public AudioSource radiationSource;

    private void Awake()
    {
        EventManager.StartListening("Jump", OnJump);
        EventManager.StartListening("Land", OnLand);

        EventManager.StartListening("LowRadiationEnter", OnLowRadiationEntered);
        EventManager.StartListening("MediumRadiationEnter", OnMediumRadiationEntered);
        EventManager.StartListening("LowRadiationExit", OnRadiationExited);
        EventManager.StartListening("MediumRadiationExit", OnRadiationExited);
    }

    private void OnJump()
    {
        fxSource.PlayOneShot(jump);
    }

    private void OnLand()
    {
        fxSource.PlayOneShot(land);
    }

    private void OnLowRadiationEntered()
    {
        PlayRadiation(radiationLow);
    }

    private void OnMediumRadiationEntered()
    {
        PlayRadiation(radiationMedium);
    }

    private void OnRadiationExited()
    {
        radiationSource.Stop();
    }

    private void PlayRadiation(AudioClip radiation)
    {
        radiationSource.clip = radiation;
        radiationSource.Play();
    }

    private void Update() { }
}
