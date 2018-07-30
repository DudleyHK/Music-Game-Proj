using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>

/// </summary>
public class GenerateMesh : ScriptableObject
{
    private static Vector3 initialPos = new Vector3(0, 0, -250);


    public static GameObject CreatePlane(GameObject _tilePrefab, Material _mat, float _width, float _length)
    {
        var mf = _tilePrefab.GetComponent<MeshFilter>();
        var mr = _tilePrefab.GetComponent<MeshRenderer>();

        var mesh = new Mesh();

        mesh.vertices = new Vector3[]
        {
           new Vector3(initialPos.x, 0, initialPos.z),
           new Vector3(initialPos.x + _width, 0, initialPos.z),
           new Vector3(initialPos.x + _width, 0, initialPos.z + _length),
           new Vector3(initialPos.x, 0, initialPos.z + _length)
        };

        mesh.uv = new Vector2[]
        {
            new Vector2(1, 0),
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };

        mesh.triangles = new int[] { 2, 1, 0, 3, 2, 0 };

        mf.mesh = mesh;

        mr.material = _mat;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();


        // TODO: Create object factory for RoadTiles which calls this fuction. 
        //         - RoadTileFactory adds the required Components.
        _tilePrefab.AddComponent<BoxCollider>();


        return _tilePrefab;
    }



    public static GameObject AttachPlane(GameObject _tilePrefab, Material _mat, Mesh _prevMesh, float _width, float _length)
    {
        var mf = _tilePrefab.GetComponent<MeshFilter>();
        var mr = _tilePrefab.GetComponent<MeshRenderer>();

        var mesh = new Mesh();


        mesh.vertices = new Vector3[]
        {
           new Vector3(_prevMesh.vertices[0].x, 0, _prevMesh.vertices[0].z + _length),
           new Vector3(_width, 0, _prevMesh.vertices[0].z + _length),
           new Vector3(_width, 0, _prevMesh.vertices[0].z + (_length * 2)),
           new Vector3(0, 0,  _prevMesh.vertices[0].z + (_length * 2))
        };

        mesh.uv = new Vector2[]
        {
            new Vector2(1, 0),
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };

        mesh.triangles = new int[] { 2, 1, 0, 3, 2, 0 };

        mf.mesh = mesh;

        mr.material = _mat;


        mesh.RecalculateBounds();
        mesh.RecalculateNormals();



        // TODO: Create object factory for RoadTiles which calls this fuction. 
        //         - RoadTileFactory adds the required Components.
        _tilePrefab.AddComponent<BoxCollider>();

        return _tilePrefab;
    }
}
