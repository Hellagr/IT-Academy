using System.Collections.Generic;
using UnityEngine;

public class CubeCreateion : MonoBehaviour
{
    [SerializeField] List<Material> materials;
    [SerializeField] float randomSpotDistance = 3f;
    public GameObject previousObject;
    public GameObject movingObject;

    GameObject staticObject;
    Mesh mesh;
    MeshRenderer meshRenderer;
    Quaternion rotationOfMovement;
    Vector3 lastObjectPos;
    float highOfObject;
    public float top = 0.25f;
    public float bottom = -0.25f;
    public float right = 0.5f;
    public float left = -0.5f;

    Vector3 directionOfObject;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.SetMaterials(materials);
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = GenerateVertices(transform.position);
        mesh.triangles = GenerateTriangles();
        mesh.RecalculateNormals();
        highOfObject = top * 2;
        lastObjectPos = transform.position;
        staticObject = gameObject;
        CreateARandomCube();
    }

    Vector3[] GenerateVertices(Vector3 positionOfObj)
    {
        return new Vector3[]
        {
            new Vector3(-0.5f, -0.25f, -0.5f) + positionOfObj, //0
            new Vector3(-0.5f, -0.25f, 0.5f) + positionOfObj, //1 
            new Vector3(0.5f, -0.25f, -0.5f) + positionOfObj, //2 
            new Vector3(0.5f, -0.25f, 0.5f) + positionOfObj, //3
            new Vector3(0.5f, 0.25f,  0.5f) + positionOfObj, //4 
            new Vector3(0.5f, 0.25f, -0.5f) + positionOfObj, //5
            new Vector3(-0.5f, 0.25f, 0.5f) + positionOfObj, //6
            new Vector3(-0.5f, 0.25f, -0.5f) + positionOfObj, //7
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

    public void CreateARandomCube()
    {
        previousObject = previousObject != null ? movingObject : staticObject;
        var newHighOfObject = lastObjectPos.y + highOfObject;

        Vector3 posForNewObj = CalculateNewPosition(newHighOfObject);
        CalculateNewRotation(posForNewObj);
        CreateNewObject(posForNewObj);
    }


    public Vector3 CalculateNewPosition(float newHighOfObject)
    {
        Vector3 posForNewObj;

        //var prevObjectMeshComponent = previousObject.GetComponent<MeshFilter>().mesh;
        //var staticObjectMeshComponent = staticObject.GetComponent<MeshFilter>().mesh;
        //var prevObjectCenter = prevObjectMeshComponent.bounds.max;
        //var staticObjectCenter = staticObjectMeshComponent.bounds.max;

        //var prevObjectCnter = previousObject != null ? prevObjectCenter : staticObjectCenter;

        if (Random.Range(0, 2) == 0)
        {
            posForNewObj = Random.Range(0, 2) == 0 ? new Vector3(randomSpotDistance, newHighOfObject, previousObject.transform.position.z) : new Vector3(-randomSpotDistance, newHighOfObject, previousObject.transform.position.z);
        }
        else
        {
            posForNewObj = Random.Range(0, 2) == 0 ? new Vector3(previousObject.transform.position.x, newHighOfObject, randomSpotDistance) : new Vector3(previousObject.transform.position.x, newHighOfObject, -randomSpotDistance);
        }
        lastObjectPos = posForNewObj;
        return posForNewObj;
    }

    private void CalculateNewRotation(Vector3 posForNewObj)
    {
        Vector2 objectPosition = new Vector2(posForNewObj.x, posForNewObj.z);
        Vector2 targetPosition = new Vector2(previousObject.transform.position.x, previousObject.transform.position.z);

        //Vector2 vectorToCenter = targetPosition - objectPosition;
        directionOfObject = new Vector3(targetPosition.x - objectPosition.x, 0f, targetPosition.y - objectPosition.y);

        // float desiredAngleInRad = Mathf.Atan2(vectorToCenter.x, vectorToCenter.y);
        //float desiredAngleInDegree = desiredAngleInRad * Mathf.Rad2Deg;

        // rotationOfMovement = Quaternion.Euler(0f, desiredAngleInDegree, 0f);
    }

    private void CreateNewObject(Vector3 posForNewObj)
    {
        movingObject = new GameObject("newSlidingObject", typeof(MeshFilter), typeof(MeshRenderer));

        var meshRendererNewObj = movingObject.GetComponent<MeshRenderer>();
        meshRendererNewObj.material = materials[0];
        var meshNewObj = movingObject.GetComponent<MeshFilter>().mesh;
        meshNewObj.vertices = GenerateVertices(movingObject.transform.position);
        meshNewObj.triangles = GenerateTriangles();
        meshNewObj.RecalculateNormals();

        movingObject.transform.position = posForNewObj;
        //movingObject.transform.rotation = rotationOfMovement;
    }

    //public void RecreateCube()
    //{
    //    var meshNewObj = movingObject.GetComponent<MeshFilter>().mesh;
    //    //Debug.Log(movingObject.transform.position);
    //    meshNewObj.vertices = RecreateVertices(movingObject.transform.position);
    //    meshNewObj.triangles = GenerateTriangles();
    //    meshNewObj.RecalculateNormals();
    //}

    //Vector3[] RecreateVertices(Vector3 positionOfObj)
    //{
    //    return new Vector3[]
    //    {
    //        new Vector3(-0.5f, -0.25f, -0.5f), //0
    //        new Vector3(-0.5f, -0.25f, 0.5f), //1 
    //        new Vector3(0.5f, -0.25f, -0.5f), //2 
    //        new Vector3(0.5f, -0.25f, 0.5f), //3
    //        new Vector3(0.5f, 0.25f,  0.5f), //4 
    //        new Vector3(0.5f, 0.25f, -0.5f), //5
    //        new Vector3(-0.5f, 0.25f, 0.5f), //6
    //        new Vector3(-0.5f, 0.25f, -0.5f), //7
    //    };
    //}

    public void CreateNewObject()
    {
        movingObject = Instantiate(movingObject);
    }

    void Update()
    {
        if (movingObject != null)
        {
            movingObject.transform.Translate(directionOfObject * 0.005f, Space.Self);
        }
    }
}