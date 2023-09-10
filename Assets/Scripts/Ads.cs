using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{

    const string ID = "3670389";
    const string PLACEMENTID = "video";
    const float TIMETOSHOWADS = 180;


    private static float timer;
    

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (Advertisement.isSupported) Advertisement.Initialize(ID, false);
        timer = 0;
    }

    public static void ShowAds(float time)
    {
        timer += time;
        if(timer >= TIMETOSHOWADS)
        {
            timer = 0;
            if (Advertisement.IsReady(PLACEMENTID))
            {
                Advertisement.Show(PLACEMENTID);
            }
        }
    }
    
}
