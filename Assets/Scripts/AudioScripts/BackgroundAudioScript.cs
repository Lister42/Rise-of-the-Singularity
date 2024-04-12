using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudioScript : MonoBehaviour
{
    private AudioSource _source;
    public AudioClip DefaultAmbience;
    public AudioClip Track1;
    public AudioClip Track2;
    public AudioClip Track3;
    public AudioClip Track4;
    public AudioClip Track5;
    bool playNewTrack;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        playNewTrack = false;
        StartCoroutine(beginningMusic());
    }

    // Update is called once per frame
    void Update()
    {
        if (playNewTrack)
        {
            StartCoroutine(backgroundMusicHandler());
        }
    }

    IEnumerator backgroundMusicHandler()
    {
        playNewTrack = false;
        _source.PlayOneShot(DefaultAmbience);
        yield return new WaitForSeconds(95);
        int randTrack = Random.Range(0, 5);
        if (randTrack == 0)
        {
            _source.PlayOneShot(Track1);
            yield return new WaitForSeconds(195);
        } else if (randTrack == 1) 
        {
            _source.PlayOneShot(Track2);
            yield return new WaitForSeconds(295);
        } else if (randTrack == 2)
        {
            _source.PlayOneShot(Track3);
            yield return new WaitForSeconds(150);
        } else if (randTrack == 3)
        {
            _source.PlayOneShot(Track4);
            yield return new WaitForSeconds(260);
        } else
        {
            _source.PlayOneShot(Track5);
            yield return new WaitForSeconds(190);
        }
        playNewTrack = true;
    }

    IEnumerator beginningMusic()
    {
        _source.PlayOneShot(Track1);
        yield return new WaitForSeconds(195);
        playNewTrack = true;
    }
}
