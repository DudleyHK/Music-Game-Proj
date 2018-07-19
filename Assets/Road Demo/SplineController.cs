using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JPBotelho;

public class SplineController : MonoBehaviour
{
    public CatmullRom Spline;

    public List<Transform> ControlPoints = new List<Transform>();

    public bool ClosedLoop = false;


    // Use this for initialization
    void Start ()
	{
        Spline = new CatmullRom(ControlPoints.ToArray(), 1, false);
	}

	// Update is called once per frame
	void Update ()
	{
        var controlPoints = ControlPoints.ToArray();

        Spline.Update(controlPoints);
        Spline.Update(1, ClosedLoop);
        Spline.DrawSpline(Color.white);
    }
}