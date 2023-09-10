using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{

    public float startX;
    public float speed;

    [SerializeField]
    GameObject glText;

    Transform c;
    Transform b;

    Vector3 cPos;
    Vector3 bPos;

    void Start()
    {
        Camera.main.GetComponent<Animator>().enabled = false;
        c = Camera.main.transform;
        b = GameObject.FindGameObjectWithTag("background").transform;
        c.position = new Vector3(startX, 0, -10);
        b.position = new Vector3(startX, 0, 2);
        cPos = new Vector3(0, 0, -10);
        bPos = new Vector3(0, 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.isPaused)
        {
            if (c.position.x != 0 && b.position.x != 0)
            {
                c.position = Vector3.MoveTowards(c.position, cPos, Time.deltaTime * speed);
                b.position = Vector3.MoveTowards(b.position, bPos, Time.deltaTime * speed);
            }
            else
            {
                Destroy(glText);
                Camera.main.GetComponent<Animator>().enabled = true;
                Destroy(gameObject);
            }
        }
    }
}
