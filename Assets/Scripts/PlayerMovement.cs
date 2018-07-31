using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
		public enum SpeedChangeType { Accelerate, Deccelerate }

	public delegate void OnChangeSpeed(SpeedChangeType _type, float _val);
	public static event OnChangeSpeed onChangeSpeed;

	[HideInInspector] public Rigidbody Rigidbody;

	public float Acceleration;
	public float Friction;
	public float Drift;



	void Start ()
	{
			Rigidbody = GetComponent<Rigidbody>();
	}


    // Player Movement
    // - Little boost forwards and backwards when the player presses down. 
    // - Not so slidy over the floor. 
    // - Jumping? Attacking? Funny Animations


    void FixedUpdate()
    {
        var hor = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");

        if(hor > 0)
        {
            Rigidbody.velocity += transform.right * Drift * Time.fixedDeltaTime;
        }

        if(hor < 0)
        {
            Rigidbody.velocity -= transform.right * Drift * Time.fixedDeltaTime;
        }
    }
}
