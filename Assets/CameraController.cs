﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
    }
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = player.transform.position.x;
        pos.y = player.transform.position.y;
        transform.position = pos;
        
    }
}