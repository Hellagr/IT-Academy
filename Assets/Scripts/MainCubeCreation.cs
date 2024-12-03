using System.Collections.Generic;
using UnityEngine;

public class MainCubeCreateion : MonoBehaviour
{
    [SerializeField] List<Material> materials;
    Mesh mesh;
    MeshRenderer meshRenderer;
    Quaternion rotationOfMovement;
    public float highOfObject;
    public float top = 0.25f;
    public float bottom = -0.25f;
    public float right = 0.5f;
    public float left = -0.5f;

    Vector3 directionOfObject;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.SetMaterials(materials);
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = GenerateVertices(transform.position);
        mesh.triangles = GenerateTriangles();
        mesh.RecalculateNormals();
        highOfObject = meshRenderer.bounds.max.y * 2;
    }

    Vector3[] GenerateVertices(Vector3 positionOfObj)
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

    int[] GenerateTriangles()
    {
        return new int[]
        {
            //looking from top
            0,3,1,0,2,3, //bottom
            3,2,4,4,2,5, //right
            5,6,4,5,7,6, //top
            0,1,6,6,7,0, //left
            3,4,6,6,1,3, //forward
            0,7,5,0,5,2 //backward
        };
    }
}