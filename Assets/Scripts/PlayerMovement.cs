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




	void FixedUpdate ()
	{
			var hor = Input.GetAxis("Horizontal");
			var vert = Input.GetAxis("Vertical");

			if(hor > 0)
			{
					Rigidbody.velocity += transform.right * Drift * Time.fixedDeltaTime;
			}
			else if(hor < 0)
			{
					Rigidbody.velocity -= transform.right * Drift * Time.fixedDeltaTime;
			}

			if(vert > 0)
			{
					if(onChangeSpeed != null)
						onChangeSpeed(SpeedChangeType.Accelerate, vert);
			}
			else if(vert < 0)
			{
					if(onChangeSpeed != null)
						onChangeSpeed(SpeedChangeType.Deccelerate, vert);
			}
		}
}
