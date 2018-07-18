using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Attach this script to he turner
/// </summary>
public class RoadMover : MonoBehaviour {

    [HideInInspector]
    public GameObject Target;
    public float Speed = 0.01f;

    [Header("Road End Points")]
    public int TargetBoundMin;
    public int TargetBoundMax;

    private int dir = 1;






    // Use this for initialization
    void Start ()
    { 
        Target = this.gameObject;
		
	}
	
	// Update is called once per frame
	void Update () {

        var tPos = Target.transform.position;

        tPos.x += Speed * dir * Time.deltaTime;

        if(tPos.x <= TargetBoundMin)
        {
            dir = 1;
        }

        if(tPos.x >= TargetBoundMax)
        {
            dir = -1;
        }
    }
}
