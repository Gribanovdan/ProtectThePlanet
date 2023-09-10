using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseManager : MonoBehaviour
{
    
    public static bool isPaused;

    public delegate void OnPause(bool ispaused);
    //public event OnPause pauseChanged; 

    [SerializeField]
    GameObject pausePanel;
    [SerializeField]
    Text lbl_score;

    LooseManager lm;


    private void Awake()
    {
        isPaused = false;
    }
    void Start()
    {
        lm = GameObject.Find("lm").GetComponent<LooseManager>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) UnPause();
            else Pause();
        }

        if (Input.GetKeyDown(KeyCode.Home)||Input.GetKeyDown(KeyCode.Menu))
        {
            if (!isPaused) Pause();
        }

    }

    public void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        lbl_score.text = "Score: " + lm.count;

     //   pauseChanged?.Invoke(isPaused);
    }

    public void UnPause()
    {
        isPaused = false;
        pausePanel.SetActive(false);
    //    pauseChanged?.Invoke(isPaused);
    }

}
