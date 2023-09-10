using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnable : MonoBehaviour
{
    [SerializeField] private GameObject[] soundSources;

    private void Start()
    {
        UpdateSoundSources();
    }
    public void UpdateSoundSources()
    {
        foreach(GameObject soundSource in soundSources)
        {
            soundSource.GetComponent<AudioSource>().enabled = Settings.SoundsAllowed;
        }
    }

    
}
