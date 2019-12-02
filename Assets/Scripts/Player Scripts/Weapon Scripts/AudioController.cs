using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] clips;
    public float delayBetweenClips;

    private bool canPlay;
    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        canPlay = true;
    }


    public void Play()
    {
        if (!canPlay)
            return;

        Invoke("CanPlayAgain", delayBetweenClips);

        canPlay = false;

        int clipIndex = Random.Range(0, clips.Length);
        AudioClip clip = clips[clipIndex];
        source.PlayOneShot(clip);
    }

    public void CanPlayAgain()
    {
        canPlay = true;
    }
}
