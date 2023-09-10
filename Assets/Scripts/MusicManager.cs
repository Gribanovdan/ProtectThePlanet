using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] music;

    AudioSource asc;
    int prevClipInd;

    float timer;
    float curClipLength;

    void Start()
    {
        asc = GetComponent<AudioSource>();
        SetNewMusic();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.isPaused)
        {
            timer += Time.deltaTime;
            if (timer >= curClipLength) SetNewMusic();
        }
    }

    void SetNewMusic()
    {
        if (Settings.MusicAllowed)
        {
            int newClipInd = Random.Range(0, music.Length);
            if (newClipInd == prevClipInd && music.Length != 1) SetNewMusic();
            else
            {
                timer = 0;
                asc.clip = music[newClipInd];
                curClipLength = asc.clip.length;
                prevClipInd = newClipInd;
                asc.Play();
            }
        }
    }

    public void SetNewMusic(AudioClip cl)
    {
        if (Settings.MusicAllowed)
        {
            asc.clip = cl;
            curClipLength = asc.clip.length;
            timer = 0;
            asc.Play();
        }
    }

}
