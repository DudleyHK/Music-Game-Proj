using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RoadGenerator : MonoBehaviour
{
    // Set the vertices positions
    // Colour tiles.
    // Change tones based on position.
    // ####### ........ ######
    // ####### ........ ######
    // ####### ........ ######
    // ####### ........ ######


    public GameObject Player;

    [Header("Grid Details")]
    public float Spread = 0.1f;
    public Vector3 InitialPos;

    [Header("Gizmos Details")]
    public bool Debug = true;
    public float GizmosSize = 0.1f;


    private List<List<char>> tileGrid = new List<List<char>>();

    private Mesh mesh;
    private MeshFilter mf;

    Vector3[] vertices;
    List<Vector2> uvs = new List<Vector2>();


    private char road = '.';
    private char grass = '#';



    // private string file = "Straight-Road.txt";
    private string file = "Straight-Road.txt";



    // Generate 1 mesh which the player sits on. 
    // Generate another mesh behind player. 

    private void Awake()
    {
        Initiate();
        mf.gameObject.transform.Rotate(new Vector3(90, 0, 0));
    }


    private void Initiate()
    {
        ReadFile();

        var xGridSize = tileGrid.Count;
        var yGridSize = tileGrid[0].Count;

        vertices = new Vector3[(xGridSize + 1) * (yGridSize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Color[] colors = new Color[vertices.Length];

        int idx = 0;
        for(int y = 0; y <= yGridSize; y++)
        {
            for(int x = 0; x <= xGridSize; x++)
            {
                var _x = (x + (x * Spread));
                var _y = (y + (y * Spread));

                vertices[idx++] = new Vector3(_x, _y);
            }
        }

        int[] triangles = new int[xGridSize * yGridSize * 6];
        for(int ti = 0, vi = 0, y = 0; y < yGridSize; y++, vi++)
        {
            for(int x = 0; x < xGridSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xGridSize + 1;
                triangles[ti + 5] = vi + xGridSize + 2;
            }
        }


        

        for(int i = 0; i < vertices.Length; i++)
        {
            int j = 0;
            char tile = ' ';

            foreach(var row in tileGrid)
            {
                foreach(var t in row)
                {
                    if(j == i)
                    {
                        tile = t;
                    }
                    j++;
                }
            }

            switch(tile)
            {
                case '#':
                    colors[i] = Color.green;
                    break;

                case '.':
                    colors[i] = Color.black;
                    break;
            }
        }

        Mesh newMesh = new Mesh();
        newMesh.vertices = vertices;
        newMesh.triangles = triangles;
        newMesh.colors = colors;


        newMesh.RecalculateBounds();
        newMesh.RecalculateNormals();

        mf = GetComponent<MeshFilter>();

        mf.mesh.Clear();
        mf.mesh = newMesh;

        mf.mesh.name = file.Split('.')[0];
    }



    private void GenerateGrid()
    {

    }


    private void ReadFile()
    {
        var path = Application.dataPath + "/Resources/Road Data/" + file;

        using(var sr = new System.IO.StreamReader(path))
        {
            while(sr.Peek() >= 0)
            {
                var line = sr.ReadLine().TrimEnd();
                tileGrid.Add(new List<char>(line.ToCharArray()));
            }
        }
    }


    private void ParseLine(string _line)
    {
        foreach(var c in _line)
        {

            switch(c)
            {
                case '.':

                    break;

                case '#':
                    break;

                default:
                    break;
            }
        }
    }



    private void GenerateSingleMesh()
    {

    }


    private void OnDrawGizmos()
    {
        if(!Debug || vertices == null)
            return;

        Gizmos.color = Color.black;

        foreach(var v in vertices)
            Gizmos.DrawSphere(v, GizmosSize);
    }
}
