using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MainCubeCreateion : MonoBehaviour
{
    public List<Material> materials;
    Mesh mesh;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;
    public float highOfObject;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.convex = true;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.SetMaterials(materials);
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();
        mesh.RecalculateNormals();
        highOfObject = meshRenderer.bounds.max.y * 2;
        meshCollider.sharedMesh = mesh;
    }

    Vector3[] GenerateVertices()
    {
        return new Vector3[]
        {
            new Vector3(-0.5f, -0.25f, -0.5f), //0
            new Vector3(-0.5f, -0.25f, 0.5f), //1 
            new Vector3(0.5f, -0.25f, -0.5f), //2 
            new Vector3(0.5f, -0.25f, 0.5f), //3
            new Vector3(0.5f, 0.25f,  0.5f), //4 
            new Vector3(0.5f, 0.25f, -0.5f), //5
            new Vector3(-0.5f, 0.25f, 0.5f), //6
            new Vector3(-0.5f, 0.25f, -0.5f), //7
        };
    }

    public int[] GenerateTriangles()
    {
        return new int[]
        {
            //looking from top
            5,6,4,5,7,6, //top
            3,2,4,4,2,5, //right
            0,1,6,6,7,0, //left
            0,3,1,0,2,3, //bottom
            3,4,6,6,1,3, //forward
            0,7,5,0,5,2 //backward
        };
    }
}