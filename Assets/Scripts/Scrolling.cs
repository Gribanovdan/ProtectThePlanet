using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrolling : MonoBehaviour
{
    [SerializeField] ScrollRect scrollBar;
    [SerializeField] AudioClip soundOfCrolling;
    [SerializeField] GameObject[] scrollObjects;

    private const float OFFSET = 150;
    private const float SPEED = 500;//500
    private const float ROUNDINGOFFSET = 10; // Константа для округления, чтобы скроллинг не двигался бесконечно к точке
    private const float MAXDISTANCEX = 100; //на какое максимальное расстояние может уйти скролбар

    private int selectedIndex;
    private int prevIndex;
    private RectTransform myTransform;
    private AudioSource asc;

    private Vector2 curPositionToMove;
    private bool canMove;

    private float minX;
    private float maxX;

    private void Start()
    {
        asc = gameObject.AddComponent<AudioSource>();
        asc.playOnAwake = false;
        asc.clip = soundOfCrolling;
        myTransform = GetComponent<RectTransform>();
        selectedIndex = 0;
        prevIndex = 0;

        canMove = true;

        for (int i = 0; i < scrollObjects.Length; i++)
        {
            scrollObjects[i] = Instantiate(scrollObjects[i], myTransform, false);
            scrollObjects[i].transform.localPosition = new Vector2(i * OFFSET, 0);
        }
        scrollObjects[0].GetComponent<GunIcon>().SelectMe();
        curPositionToMove = new Vector2(myTransform.rect.width / 2 - (selectedIndex * OFFSET), myTransform.localPosition.y);
        maxX = myTransform.rect.width / 2 + MAXDISTANCEX;
        minX = myTransform.rect.width / 2 - (scrollObjects.Length - 1) * OFFSET - MAXDISTANCEX;
    }

    private void Update()
    {
        if (canMove)
        {
            myTransform.localPosition = Vector2.MoveTowards(myTransform.localPosition, curPositionToMove, Time.deltaTime * SPEED);
        }
        if(myTransform.localPosition.x < minX)
        {
            if (canMove) scrollBar.enabled = false;
            myTransform.localPosition = new Vector2(minX, myTransform.localPosition.y);
        }
        else if(myTransform.localPosition.x > maxX)
        {
            if(canMove) scrollBar.enabled = false;
            myTransform.localPosition = new Vector2(maxX, myTransform.localPosition.y);
        }
    }


    public void SelectGun()
    {
        selectedIndex = (int)Mathf.Round(((myTransform.rect.width/2)-myTransform.localPosition.x)/OFFSET);
        
        if (selectedIndex > scrollObjects.Length - 1) selectedIndex = scrollObjects.Length - 1;
        else if (selectedIndex < 0) selectedIndex = 0;

        curPositionToMove = new Vector2(myTransform.rect.width / 2 - (selectedIndex * OFFSET), myTransform.localPosition.y);

        if (selectedIndex != prevIndex)
        {
            if (Settings.SoundsAllowed)
            {
                asc.Stop();
                asc.Play();
            }
            scrollObjects[prevIndex].GetComponent<GunIcon>().DeselectMe();
            scrollObjects[selectedIndex].GetComponent<GunIcon>().SelectMe();

            prevIndex = selectedIndex;
        }
    }

    public void PointerDown()
    {
        scrollBar.enabled = true;
        canMove = false;
    }

    public void PointerUp()
    {
        //scrollBar.enabled = false;
        canMove = true;
    }
}
