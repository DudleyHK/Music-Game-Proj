using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum InputDir
{
    None = 0,
    Forward = 1,
    Backward = 2,
    Left = 4,
    Right = 8
}


public class GenerateOnStart : MonoBehaviour
{
    public List<Road> rdTiles;
    public GameObject tilePrefab;
    public Material road;
    private float killZ = -260;


    [Header("Physics Vars")]
    public float EnginePower;
    public float BrakePower;
    public float Drag;
    public float RR; // Rolling Resistance... This must be 30times the value of Drag
    public float Mass; // in KG


    [Header("Helpers")]
    public Vector3 acc; // Meters per sec
    public Vector3 brake;
    public Vector3 vel;
    public Vector3 dtvel;
    public Vector3 dtspeed;
    public float speed;

    [Space]

    public Vector3 lf; // longitudinal force in Newtons
    public Vector3 traction;
    public Vector3 drag;
    public Vector3 rr; // rolling resistance

    [Space]


    public float turn;
    public float gas;



    // Use this for initialization
    private void Start()
    {
        var startTiles = new Road[5];

        startTiles[0] = GenerateMesh.CreatePlane(Instantiate(tilePrefab, Vector3.zero, Quaternion.identity), road, 30, 10).GetComponent<Road>();

        startTiles[1] = GenerateMesh.AttachPlane(
            Instantiate(tilePrefab, Vector3.zero, Quaternion.identity), road, startTiles[0].GetComponent<MeshFilter>().sharedMesh, 30, 10).GetComponent<Road>();

        startTiles[2] = GenerateMesh.AttachPlane(
            Instantiate(tilePrefab, Vector3.zero, Quaternion.identity), road, startTiles[1].GetComponent<MeshFilter>().sharedMesh, 30, 10).GetComponent<Road>();

        startTiles[3] = GenerateMesh.AttachPlane(
            Instantiate(tilePrefab, Vector3.zero, Quaternion.identity), road, startTiles[2].GetComponent<MeshFilter>().sharedMesh, 30, 10).GetComponent<Road>();

        startTiles[4] = GenerateMesh.AttachPlane(
            Instantiate(tilePrefab, Vector3.zero, Quaternion.identity), road, startTiles[3].GetComponent<MeshFilter>().sharedMesh, 30, 10).GetComponent<Road>();

        rdTiles = new List<Road>(startTiles);
    }



    // Update is called once per frame
    void Update()
    {
        PlayerInput();

        GenerateRoad();
    }

    private void FixedUpdate()
    {
        MoveTiles(gas);
    }


    private void PlayerInput()
    {
        turn = Input.GetAxis("Horizontal");
        gas = Input.GetAxis("Vertical");

    }


    private void MoveTiles(float _gas)
    {
        foreach(var road in rdTiles)
        {
            var vertices = road.mf.mesh.vertices;

            for(int i = 0; i < vertices.Length; i++)
            {
                var vertex = vertices[i];

                vertex = ApplyPhysics(vertex, _gas);

                vertices[i] = vertex;
            }

            road.mf.mesh.vertices = vertices;

            road.mf.mesh.RecalculateNormals();
            road.mf.mesh.RecalculateBounds();
        }
    }


    private Vector3 ApplyPhysics(Vector3 _vertex, float _gas)
    {
        var pos = _vertex;

        lf = LongitudalForce(_gas);

        acc = lf / Mass;

        dtvel = acc * Time.deltaTime;
        vel += dtvel;

        dtspeed = vel * Time.deltaTime;
        pos += dtspeed;

        speed = vel.magnitude;

        return pos;
    }


    private Vector3 LongitudalForce(float _gas)
    {
        // Calculate Forces.
        if(_gas > 0)
        {
            traction = Vector3.back * (EnginePower * _gas);
        }
        else if(_gas < 0)
        {
            traction = Vector3.back * (BrakePower * _gas);
        }
        else
        {
            traction = Vector3.back * _gas;
        }

        drag = -Drag * vel * speed;
        rr = -RR * vel;

        lf = traction + drag + rr;

        return lf;
    }


    private void GenerateRoad()
    {
        var vertices = rdTiles[0].mf.mesh.vertices;

        for(int j = 0; j < vertices.Length; j++)
        {
            var vertex = vertices[j];

            if(vertex.z < killZ)
            {
                rdTiles.Add(GenerateMesh.AttachPlane(
                    Instantiate(tilePrefab, Vector3.zero, Quaternion.identity), road,
                    rdTiles[rdTiles.Count - 1].mf.sharedMesh, 30, 10).GetComponent<Road>());

                Destroy(rdTiles[0].gameObject);
                rdTiles.RemoveAt(0);
                return;
            }
        }
    }
}
