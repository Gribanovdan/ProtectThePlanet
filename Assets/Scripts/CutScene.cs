using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    //[SerializeField] Image captain;
    [SerializeField] GameObject[] sceneObjects;
    [SerializeField] float speed;

    private bool isPressed;

    float loadTimer;
    void Start()
    {
        loadTimer = 0;
        loadTimer = 0;
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isPressed = true;
        }
        if (!isPressed)
        {
            //captain.color = Color.Lerp(captain.color, new Color(captain.color.r, captain.color.g,captain.color.b, 1), Time.deltaTime * speed/1.5f);
            foreach (GameObject go in sceneObjects)
            {
                if (go.GetComponent<Image>())
                {
                    Image i = go.GetComponent<Image>();
                    i.color = Color.Lerp(i.color, new Color(i.color.r, i.color.g, i.color.b, 1), Time.deltaTime*speed);

                }
                else if (go.GetComponent<Text>())
                {

                    Text i = go.GetComponent<Text>();
                    i.color = Color.Lerp(i.color, new Color(i.color.r, i.color.g, i.color.b, 1), Time.deltaTime*speed);
                }
            }
        }
        else
        {
            loadTimer += Time.deltaTime;
            //captain.color = Color.Lerp(captain.color, new Color(captain.color.r, captain.color.g, captain.color.b, 0), Time.deltaTime * speed *1.5f);
            foreach (GameObject go in sceneObjects)
            {
                if (go.GetComponent<Image>())
                {
                    Image i = go.GetComponent<Image>();
                    i.color = Color.Lerp(i.color, new Color(i.color.r, i.color.g, i.color.b, 0), Time.deltaTime);


                }
                else if (go.GetComponent<Text>())
                {

                    Text i = go.GetComponent<Text>();
                    i.color = Color.Lerp(i.color, new Color(i.color.r, i.color.g, i.color.b, 0), Time.deltaTime);
                }
            }
            if (loadTimer >= 1.5f)
            {
                loadTimer = -10000;
                SceneManager.LoadSceneAsync(3);
                SceneManager.LoadSceneAsync(1);
                SceneManager.UnloadSceneAsync(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
