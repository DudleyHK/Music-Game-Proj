using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GenerateMesh : ScriptableObject
{
    public static List<GameObject> meshes = new List<GameObject>();

    private static Vector3 initialPos = new Vector3(0, 0, -250);


    public static GameObject CreatePlane(float _width, float _length, Material _mat)
    {
        var go = new GameObject("Plane");
        var mf = go.AddComponent<MeshFilter>();
        var mr = go.AddComponent<MeshRenderer>();

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

        go.AddComponent<BoxCollider>();
        go.AddComponent<Rigidbody>();


        mr.material = _mat;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        meshes.Add(go);
        return go;
    }



    public static GameObject AttachPlane(float _width, float _length, Material _mat)
    {
        var last = meshes[meshes.Count - 1].GetComponent<MeshFilter>().mesh;
        
        var go = new GameObject("Plane");
        var mf = go.AddComponent<MeshFilter>();
        var mr = go.AddComponent<MeshRenderer>();

        var mesh = new Mesh();


        mesh.vertices = new Vector3[]
        {
           new Vector3(last.vertices[0].x, 0, last.vertices[0].z + _length),
           new Vector3(_width, 0, last.vertices[0].z + _length),
           new Vector3(_width, 0, last.vertices[0].z + (_length * 2)),
           new Vector3(0, 0,  last.vertices[0].z + (_length * 2))
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

        go.AddComponent<BoxCollider>();
        go.AddComponent<Rigidbody>();

        mr.material = _mat;


        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        meshes.Add(go);



        return default(GameObject);
    }



    public static void RemoveAt(int i)
    {
        Debug.Log(" dsd ");
        Destroy(meshes[i]);
        meshes.RemoveAt(i);
    }
}
