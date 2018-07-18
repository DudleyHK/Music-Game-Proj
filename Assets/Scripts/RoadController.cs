using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public Vector3 StartPoint = new Vector3(0, -5, 0);
    public ushort NumberOfPoints = 5;

    private LineRenderer lineRenderer;




    private void Awake()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }


    private void Start()
    {
        var start = new Vector3(0, 5, 0);
        var end = new Vector3(0, -5, 0);

        var i = 0;

        var currentPos = start;

        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(i++, start);


        while(currentPos != end)
        {
            lineRenderer.positionCount += 1;
            currentPos.y -= 1;

            lineRenderer.SetPosition(i++, currentPos);
        }


    }
}
