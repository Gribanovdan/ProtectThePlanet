using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragPanel : MonoBehaviour
{
    [SerializeField] private float loopTime;
    [SerializeField] private GameObject tapImage;
    [SerializeField] private GameObject text;

    private Image img;
    private Text txt;

    float time;

    void Start()
    {
        txt = text.GetComponent<Text>();
        img = tapImage.GetComponent<Image>();
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - time < loopTime)
        {
            img.CrossFadeAlpha(0, loopTime, false);
        }
        else
        {
            img.CrossFadeAlpha(1, loopTime, false);
        }
        if (Time.time - time > 2 * loopTime) time = time = Time.time;
        if (Input.GetMouseButtonDown(0)) Destroy(gameObject);
    }
}
