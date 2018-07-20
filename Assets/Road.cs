using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public delegate void Hit();
    public static event Hit hit;

    public float Speed;

    [HideInInspector] public Rigidbody rb;




    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
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
