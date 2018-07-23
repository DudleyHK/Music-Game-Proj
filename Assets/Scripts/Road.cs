﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public delegate void Hit();
    public static event Hit hit;

    public float Speed;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Bounds bounds;
    [HideInInspector] public MeshFilter mf;





    // Use this for initialization
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bounds = GetComponent<Renderer>().bounds;
        mf = GetComponent<MeshFilter>();
    }

    private void Start()
    {
         
    }


    private void FixedUpdate()
    {
        rb.velocity -= transform.forward * Speed * Time.fixedDeltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Destroyer")
        {
            if(hit != null)
                hit();
        }
    }
}