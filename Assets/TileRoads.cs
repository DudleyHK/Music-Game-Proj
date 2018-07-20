using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRoads : MonoBehaviour 
{
    [SerializeField] List<GameObject> tileRoads = new List<GameObject>();

    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject startTile;

    private Vector3 initPos;
    private Vector3 genPos;



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
        if(Vector3.Distance(tileRoads[tileRoads.Count - 1].transform.position, genPos) <= 0.1f)
        {
            GenerateTile(initPos);
        }
    }



    private void Init()
    {
        for(int i = 0; i < 2; i++)
        {
            var pos = tileRoads[tileRoads.Count - 1].transform.position;
            var zSize = tileRoads[tileRoads.Count - 1].GetComponent<Renderer>().bounds.size.z;

            var nextPos = new Vector3(pos.x, pos.y, pos.z + zSize);

            GenerateTile(nextPos);
        }    

        genPos = tileRoads[1].transform.position;
        initPos = tileRoads[2].transform.position;
    }



    private void GenerateTile(Vector3 _pos)
    {
        var tile = Instantiate(tilePrefab, _pos, Quaternion.identity);
        tile.GetComponent<Rigidbody>().velocity = tileRoads[0].GetComponent<Rigidbody>().velocity;

        tileRoads.Add(tile);
    }

    
    private void Hit()
    {
        GenerateTile(initPos);
    }

}
