using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public static bool MusicAllowed;
    public static bool SoundsAllowed;

    public void AllowMusic(bool allow)
    {
        MusicAllowed = !allow;
        PlayerPrefs.SetInt("musicAllowed", MusicAllowed?1:0);
        GameObject.Find("music").GetComponent<AudioSource>().enabled = MusicAllowed;
    }

    public void AllowSounds(bool allow)
    {
        SoundsAllowed = !allow;
        PlayerPrefs.SetInt("soundsAllowed", SoundsAllowed ? 1 : 0);
    }

    public void SetToggles()
    {
        GameObject btnMusic = GameObject.Find("btnMusic");
        GameObject btnSounds = GameObject.Find("btnSounds");
        if (btnMusic && btnSounds)
        {
            LoadSettings();
            btnMusic.GetComponent<Toggle>().isOn = !MusicAllowed;
            btnSounds.GetComponent<Toggle>().isOn = !SoundsAllowed;
        }
    }

    public void LoadSettings()
    {
        MusicAllowed = PlayerPrefs.GetInt("musicAllowed", 1) == 1 ? true : false;
        SoundsAllowed = PlayerPrefs.GetInt("soundsAllowed", 1) == 1 ? true : false;
    }

    public void ResetRecord()
    {
        PlayerPrefs.SetInt("record", 0);
        SceneManager.LoadScene(0);
    }
}
