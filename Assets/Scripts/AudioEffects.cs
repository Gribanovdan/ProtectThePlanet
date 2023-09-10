using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffects : MonoBehaviour
{



    [SerializeField]
    AudioClip EarthBoom;

    [SerializeField]
    AudioClip MoonBoom;

    AudioSource asc;
    void Start()
    {
        asc = GetComponent<AudioSource>();
    }

    public void BoomEarth()
    {
        asc.volume = 0.5f;
        asc.Stop();
        asc.clip = EarthBoom;
        asc.Play();
    }

    public void BoomMoon()
    {
        asc.Stop();
        asc.volume = 0.5f;
        asc.clip = MoonBoom;
        asc.Play();
    }

    public void BoomOnEarth()
    {
        asc.Stop();
        asc.volume = 0.1f;
        asc.clip = EarthBoom;
        asc.Play();
    }
}
