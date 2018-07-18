using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

	public float Speed { get; set; }

	[HideInInspector] public Rigidbody Rigidbody;


	// Use this for initialization
	void Awake () {
Rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
			Rigidbody.velocity -= transform.forward * Speed * Time.fixedDeltaTime;
	}
}
