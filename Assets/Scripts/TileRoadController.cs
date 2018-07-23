using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRoadController : MonoBehaviour 
{
    [SerializeField] List<Road> tileRoads = new List<Road>();

    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject destroyerPrefab;
    [SerializeField] Road startTile;

    private Vector3 initPos;
    private Vector3 genPos;

    private bool init = false;



    private void OnEnable()
    {
        Road.hit += Hit;
    }


    private void OnDisable()
    {
        Road.hit -= Hit;
    }



    private void Start()
    {
        tileRoads.Add(startTile);

        Init();
    }



    private void Update()
    {
        // TODO: attach to previous planes min/ max bounds of plane
        //if(Vector3.Distance(tileRoads[tileRoads.Count - 1].transform.position, genPos) <= 0.1f)
        //    GenerateTile(initPos);    
    }



    private void Init()
    {






        for(int i = 0; i < 2; i++)
        {
            var pos = tileRoads[tileRoads.Count - 1].transform.position;
            var zSize = tileRoads[tileRoads.Count - 1].bounds.size.z;

            var nextPos = new Vector3(pos.x, pos.y, pos.z + zSize);

            GenerateTile(nextPos);
        }    

        genPos = tileRoads[1].transform.position;
        initPos = tileRoads[2].transform.position;


        init = true;
    }



    private void GenerateTile(Vector3 _pos)
    {
        var road = Instantiate(tilePrefab, _pos, Quaternion.identity).GetComponent<Road>();

        road.rb.velocity = tileRoads[0].rb.velocity;

        tileRoads.Add(road);
    }

    
    private void Hit()
    {
        if(init)
            GenerateTile(initPos);
    }

}
