using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float bpm = 128.0f;
    public int numBeatsIntro = 8;
    public int numBeatsTransition = 64;
    public int numBeatsLoop = 64;
    public AudioClip intro;
    public AudioClip loop;
    public AudioSource[] audioSources = new AudioSource[2];
    public float playVolume = 0.2f;

    private double nextEventTime;
    private int flip = 0;
    private bool running = false;
    private PlaySegment inSegment;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!running)
        {
            return;
        }

        double time = AudioSettings.dspTime;

        if (time + 1.0f > nextEventTime)
        {
            if (inSegment == PlaySegment.Intro)
            {
                double upcomingEventTime = nextEventTime;
                nextEventTime += 60.0f / bpm * numBeatsIntro;

                audioSources[flip].clip = intro;
                audioSources[flip].PlayScheduled(upcomingEventTime);
                audioSources[flip].SetScheduledEndTime(nextEventTime);

                flip = 1 - flip;
            }
            else if (inSegment == PlaySegment.Transition)
            {
                double upcomingEventTime = nextEventTime;
                nextEventTime += 60.0f / bpm * numBeatsTransition;

                audioSources[flip].clip = intro;
                audioSources[flip].PlayScheduled(upcomingEventTime);
                audioSources[flip].SetScheduledEndTime(nextEventTime);

                flip = 1 - flip;

                inSegment = PlaySegment.Loop;
            }
            else if (inSegment == PlaySegment.Loop)
            {
                double upcomingEventTime = nextEventTime;
                nextEventTime += 60.0f / bpm * numBeatsLoop;

                audioSources[flip].clip = loop;
                audioSources[flip].PlayScheduled(upcomingEventTime);
                audioSources[flip].SetScheduledEndTime(nextEventTime);

                flip = 1 - flip;
            }

        }
    }

    public void StartTransition()
    {
        inSegment = PlaySegment.Transition;
    }

    public void InitMusic()
    {
        if (running)
        {
            Restart();
        }
        else
        {
            foreach (AudioSource source in audioSources)
            {
                source.volume = playVolume;
            }

            nextEventTime = AudioSettings.dspTime + 0.8f;
            running = true;
            inSegment = PlaySegment.Intro;
        }
    }

    private void Restart()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        while (audioSources[0].volume > 0)
        {
            foreach (AudioSource source in audioSources)
            {
                source.volume = source.volume - Time.deltaTime * (playVolume / 0.3f);
            }
            yield return null;
        }

        foreach (AudioSource source in audioSources)
        {
            source.Stop();
            source.volume = playVolume;
        }

        nextEventTime = AudioSettings.dspTime + 0.05f;
        running = true;
        inSegment = PlaySegment.Intro;
    }

    private enum PlaySegment { Intro, Transition, Loop };
}
