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
    public Vector3 speed, vel, pos;
    public Vector3 acc, dtSpeed, dtVel;

    public List<GameObject> tiles;
    public GameObject tilePrefab;
    public Material road;


    // Use this for initialization
    private void Start()
    {

        var startTiles = new GameObject[3];

        startTiles[0] = GenerateMesh.CreatePlane(Instantiate(tilePrefab, Vector3.zero, Quaternion.identity), road, 30, 10);

        startTiles[1] = GenerateMesh.AttachPlane(Instantiate(tilePrefab, Vector3.zero, Quaternion.identity), road, startTiles[0].GetComponent<MeshFilter>().sharedMesh, 30, 10);
        startTiles[2] = GenerateMesh.AttachPlane(Instantiate(tilePrefab, Vector3.zero, Quaternion.identity), road, startTiles[1].GetComponent<MeshFilter>().sharedMesh, 30, 10);

        tiles = new List<GameObject>(startTiles);
    }



    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        ApplyPhysics(Vector3.forward, 0.5f);


       // if(tiles[0].transform.position.z <= -50)
       // {
       //     tiles.Add(GenerateMesh.AttachPlane(
       //         Instantiate(tilePrefab, Vector3.zero, Quaternion.identity),
       //         road, tiles[tiles.Count - 1].GetComponent<MeshFilter>().sharedMesh, 30, 10));
       //
       //     Destroy(tiles[0]);
       //     tiles.RemoveAt(0);
       // }
    }


    private void PlayerInput()
    {
        var hor = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");

        //if(hor > 0)
        //    ApplyPhysics(Vector3.right, hor);
        //if(hor < 0)
        //    ApplyPhysics(Vector3.left, hor);
        //if(vert > 0)
        //    ApplyPhysics(Vector3.forward, vert);
        //if(vert < 0)
        //    ApplyPhysics(Vector3.back, vert);
    }


    private void ApplyPhysics(Vector3 _dir, float _val)
    {
        for(int i = 0; i < tiles.Count; i++)
        {
            var ms = tiles[i].GetComponent<MeshFilter>().mesh;

            var nmesh = ms;

            for(int j = 0; j < nmesh.vertices.Length; i++)
            {
                var v = nmesh.vertices[j];

                v += _dir * (_val * 1000f) * Time.deltaTime;

                ms.vertices[j] = new Vector3(v.x, v.y, v.x);

                Debug.Log("Vertex " + j + " position is - " + ms.vertices[i]);
            }

            ms = nmesh;
            
        }
    }
}
