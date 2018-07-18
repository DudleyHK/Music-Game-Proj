using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
				if(Input.GetKey(KeyCode.LeftArrow))
				{
					var position = transform.position;
					position.x -= 10 * Time.deltaTime;

					transform.position = position;
				}
	}
}
