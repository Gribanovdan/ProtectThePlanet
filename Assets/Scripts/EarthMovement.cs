using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMovement : MonoBehaviour
{

    const float ROTSPEED = 0.1f;
    const float MOVESPEED = 1f;

    Vector3 sunPos;

    void Start()
    {
        sunPos = GameObject.Find("sun").transform.position;
    }

    
    void Update()
    {
        #region rotation
        transform.Rotate(0,0,ROTSPEED);
        #endregion
    }
}
