using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{

    const float MOVESPEED = 5f;
    const float ROTSPEED = 5f;

    GameObject background;

    Vector2 moonPos;

    void Start()
    {
        moonPos = GameObject.Find("moon").transform.position;
        background = GameObject.FindGameObjectWithTag("background");
    }

    // Update is called once per frame
    void Update()
    {
        #region sunMovement
        transform.RotateAround(moonPos, new Vector3(0, 0, 1), -1*MOVESPEED * Time.deltaTime);
        #endregion
        #region backRotation
        background.transform.Rotate(0, 0, -1*ROTSPEED * Time.deltaTime);
        #endregion
    }
}
