using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    private Mesh _hexMesh;
    private List<Vector3> _vertices;
    private List<int> _triangles;

    private MeshCollider _meshCollider;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = _hexMesh = new Mesh();
        _hexMesh.name = "Hex Mesh";
        _vertices = new List<Vector3>();
        _triangles = new List<int>();
        _meshCollider = gameObject.AddComponent<MeshCollider>();
    }

    /// <summary>
    /// Used to triangulate an array of cells in the grid
    /// </summary>
    /// <param name="cells"></param>
    public void Triangulate(HexCell[] cells)
    {
        //reset the mesh in case there is something remaining from last render
        _hexMesh.Clear();
        _vertices.Clear();
        _triangles.Clear();

        for (int cellNum = 0; cellNum < cells.Length; cellNum++)
        {
            Triangulate(cells[cellNum]);
        }

        //pass the calculated vertices and triangles to the mesh to recalculate the normals
        _hexMesh.vertices = _vertices.ToArray();
        _hexMesh.triangles = _triangles.ToArray();
        _hexMesh.RecalculateNormals();
        //after calculating the triangles attach the mesh to the collider
        _meshCollider.sharedMesh = _hexMesh;
    }

    /// <summary>
    /// Creates the triangle for a single hex cell
    /// </summary>
    /// <param name="cell"></param>
    private void Triangulate(HexCell cell)
    {
        Vector3 center = cell.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(center, center + HexMetrics.corners[i], center + HexMetrics.corners[i+1]);    
        }
        
    }

    /// <summary>
    /// Given 3 vertex position create a triangle between them
    /// </summary>
    /// <param name="v1">first vertex</param>
    /// <param name="v2">second vertex</param>
    /// <param name="v3">third vertex</param>
    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        //get how many vertices i already created
        int vertexIndex = _vertices.Count;

        _vertices.Add(v1);
        _vertices.Add(v2);
        _vertices.Add(v3);

        _triangles.Add(vertexIndex);
        _triangles.Add(vertexIndex+1);
        _triangles.Add(vertexIndex+2);

    }
 }
