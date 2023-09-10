using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunIcon : MonoBehaviour
{
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite lockedSprite;
    [SerializeField] GameObject unlockText;
    private int score;
    private Image image;
    private const float SELECTSIZE = 1.1f;


    [Header("Gun discription")]
    [SerializeField] int cost;
    [SerializeField] GameObject basa;

    [Header("Parameters")]
    [SerializeField] int rotation;
    [SerializeField] int reload;
    [SerializeField] int speed;
    private GameObject parameters;

    private void Start()
    {
        parameters = GameObject.Find("parameters");

        score = PlayerPrefs.GetInt("record", 0);
        image = GetComponent<Image>();
        if (cost <= score) image.sprite = normalSprite;
        else {
            unlockText = Instantiate(unlockText, transform);
            unlockText.GetComponent<Text>().text = "TO UNLOCK REACH " + cost.ToString() + "!";
            image.sprite = lockedSprite; 
        }
    }


    public void SelectMe()
    {
        transform.localScale = new Vector3(transform.localScale.x * SELECTSIZE, transform.localScale.y * SELECTSIZE, 
            transform.localScale.z*SELECTSIZE);
        if (cost <= score)
        {
            GameObject.Find("[INTERFACE]").GetComponent<MenuController>().canPlay = true;
            GunChoice.Basa = basa;

            if(parameters)
            parameters.SetActive(true);
            GameObject.Find("rotation").GetComponent<Image>().fillAmount = 0.2f*rotation;
            GameObject.Find("reload").GetComponent<Image>().fillAmount = 0.2f* reload;
            GameObject.Find("speed").GetComponent<Image>().fillAmount = 0.2f * speed;
 
        }
        else
        {
            
            parameters.SetActive(false);
            GameObject.Find("[INTERFACE]").GetComponent<MenuController>().canPlay = false;

        }
    }

    public void DeselectMe()
    {
        transform.localScale = new Vector3(transform.localScale.x / SELECTSIZE, transform.localScale.y / SELECTSIZE,
            transform.localScale.z / SELECTSIZE);
    }

}

