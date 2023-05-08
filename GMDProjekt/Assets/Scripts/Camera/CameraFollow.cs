using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject toFollow;

    private Vector3 ofset;
    // Start is called before the first frame update
    void Start()
    {
        ofset = transform.position - toFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (toFollow != null)
        {
            transform.position = toFollow.transform.position - ofset;
        }
    }
}
