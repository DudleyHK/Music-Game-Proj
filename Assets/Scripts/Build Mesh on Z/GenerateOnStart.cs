using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateOnStart : MonoBehaviour {

    public Material road;


	// Use this for initialization
	void Start () {
		
        GenerateMesh.CreatePlane(30, 10, road);

        GenerateMesh.AttachPlane(30, 10, road);
        GenerateMesh.AttachPlane(30, 10, road);

    }
	
	// Update is called once per frame
	void Update () 
    {
	}
}
