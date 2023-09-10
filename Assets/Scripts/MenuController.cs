using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject mainPanel;
    [SerializeField]
    GameObject pText;
    [SerializeField]
    GameObject recordText;
    [SerializeField]
    GameObject pnl_back;

    public bool canPlay { get; set; }

    GameObject cam;
    GameObject bground;

    AudioSource asc;

    Text rText;

    float timer;

    Vector2 panelPos;
    Vector2 textPos;
    Vector2 camPos;
    Vector3 bPos;

    bool play;

    void Start()
    {
        asc = GameObject.Find("music").GetComponent<AudioSource>();
        cam = Camera.main.gameObject;
        bground = GameObject.FindGameObjectWithTag("background");
        timer = 0;
        play = false;
        panelPos = new Vector2(-10, mainPanel.transform.position.y);
        textPos = new Vector2(-10, pText.transform.position.y);
        rText = recordText.GetComponent<Text>();
        rText.text = "RECORD: " + PlayerPrefs.GetInt("record", 0).ToString();
        camPos = new Vector2(10, cam.transform.position.y);
        bPos = new Vector3(camPos.x, bground.transform.position.y, 2);
        GameObject.Find("settings").GetComponent<Settings>().LoadSettings();

        // Место для выбора сложности если оно не выбрано



        //
    }

    // Update is called once per frame
    void Update()
    {
        #region Animation
        if (play)
        {
            timer += Time.deltaTime;
            mainPanel.transform.position = Vector2.Lerp(mainPanel.transform.position, 
                panelPos, Time.deltaTime * 2 * speed);
            pText.transform.position = Vector2.Lerp(pText.transform.position,
                textPos, Time.deltaTime * 2 * speed);
            if (timer >= 3) {
                asc.Stop();
                SceneManager.LoadScene(2);
            }
            cam.transform.position = Vector2.MoveTowards(cam.transform.position, camPos, Time.deltaTime * speed);
            bground.transform.position = Vector3.MoveTowards(bground.transform.position, 
                bPos, Time.deltaTime * speed);
            asc.volume -= 0.005f;
        }
        #endregion
        #region OnBack
        if (Input.GetKeyDown(KeyCode.Escape)&&!play)
        {
            if (!pnl_back.activeInHierarchy) pnl_back.SetActive(true);
            else pnl_back.SetActive(false);
        }
        #endregion
    }

    public void Play()
    {
        if(canPlay)
        play = true;
    }

    

}
