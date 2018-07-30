using System.Collections;
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
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        mf = GetComponent<MeshFilter>();
    }
}
