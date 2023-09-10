using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    Transform t;
    float angle;


    void Start()
    {
        t = transform;
        angle = Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        t.Rotate(0, 0, angle);    
    }
}
