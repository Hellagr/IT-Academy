using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CubeDivider : MonoBehaviour
{
    [SerializeField] CubeGenerator cubeGenerator;
    GameManager gameManager;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void CutTheMovingObject()
    {
        GameObject prevObject = cubeGenerator.previousObject;
        GameObject movingObject = cubeGenerator.movingObject;
        GameObject restOfCube = Instantiate(movingObject);

        var prevObjectMesh = prevObject.GetComponent<MeshFilter>().mesh;
        var movingObjectMesh = movingObject.GetComponent<MeshFilter>().mesh;
        var restOfCubeMesh = restOfCube.GetComponent<MeshFilter>().mesh;

        Vector3[] vertices1 = prevObjectMesh.vertices;
        Vector3[] vertices2 = movingObjectMesh.vertices;
        Vector3[] vertices3 = movingObjectMesh.vertices;

        Vector3 prevObjMaxGlobal = prevObject.transform.TransformPoint(prevObjectMesh.bounds.max);
        Vector3 prevObjMinGlobal = prevObject.transform.TransformPoint(prevObjectMesh.bounds.min);

        float prevObjXmax = prevObjMaxGlobal.x;
        float prevObjXmin = prevObjMinGlobal.x;
        float prevObjZmax = prevObjMaxGlobal.z;
        float prevObjZmin = prevObjMinGlobal.z;

        Vector3 prevObjectCenter = prevObject.transform.position;
        Vector3 movingObjectCenter = movingObject.transform.position;

        //Is behind of previousObject
        bool isMovingObjectBehind = prevObjectCenter.x > movingObjectCenter.x || prevObjectCenter.z > movingObjectCenter.z;

        Vector3 movingObjectGlobalPosition = movingObject.transform.position;

        for (var i = 0; i < vertices2.Length; i++)
        {
            Vector3 vertexInGlobal = movingObjectGlobalPosition + vertices2[i];
            float xVertexInGlobal = vertexInGlobal.x;
            float yVertexInGlobal = vertexInGlobal.y;
            float zVertexInGlobal = vertexInGlobal.z;

            if (prevObjectCenter.z == movingObjectCenter.z)
            {
                CheckXaxis(movingObject, vertices2, vertices3, prevObjXmax, prevObjXmin, i, xVertexInGlobal, yVertexInGlobal, zVertexInGlobal, isMovingObjectBehind);
            }
            else
            {
                CheckZaxis(movingObject, vertices2, vertices3, prevObjZmax, prevObjZmin, i, xVertexInGlobal, yVertexInGlobal, zVertexInGlobal, isMovingObjectBehind);
            }
        }

        RecalculateRestOfObject(restOfCube, restOfCubeMesh, vertices3);
        RecalculateMovingObject(movingObject, movingObjectMesh, vertices2);

        gameManager.CheckIfPlayerLose(movingObject, movingObjectMesh.bounds.max.x, movingObjectMesh.bounds.min.x, movingObjectMesh.bounds.max.z, movingObjectMesh.bounds.min.z);
    }

    void CheckXaxis(GameObject movingObject, Vector3[] vertices2, Vector3[] vertices3, float prevObjXmax, float prevObjXmin, int i, float xVertexInGlobal, float yVertexInGlobal, float zVertexInGlobal, bool isMovingObjectBehind)
    {
        float xPosition = isMovingObjectBehind ? prevObjXmin : prevObjXmax;
        Vector3 globalPosition = new Vector3(xPosition, yVertexInGlobal, zVertexInGlobal);

        if (xVertexInGlobal < prevObjXmin || xVertexInGlobal > prevObjXmax)
        {
            vertices2[i] = movingObject.transform.InverseTransformPoint(globalPosition);
        }
        else
        {
            vertices3[i] = movingObject.transform.InverseTransformPoint(globalPosition);
        }
    }

    void CheckZaxis(GameObject movingObject, Vector3[] vertices2, Vector3[] vertices3, float prevObjZmax, float prevObjZmin, int i, float xVertexInGlobal, float yVertexInGlobal, float zVertexInGlobal, bool isMovingObjectBehind)
    {
        float zPosition = isMovingObjectBehind ? prevObjZmin : prevObjZmax;
        Vector3 globalPosition = new Vector3(xVertexInGlobal, yVertexInGlobal, zPosition);

        if (zVertexInGlobal < prevObjZmin || zVertexInGlobal > prevObjZmax)
        {
            vertices2[i] = movingObject.transform.InverseTransformPoint(globalPosition);
        }
        else
        {
            vertices3[i] = movingObject.transform.InverseTransformPoint(globalPosition);
        }
    }

    void RecalculateRestOfObject(GameObject restOfCube, Mesh restOfCubeMesh, Vector3[] vertices3)
    {
        var rbRestCube = restOfCube.AddComponent<Rigidbody>();
        var meshColliderRestOfCube = restOfCube.GetComponent<MeshCollider>();
        restOfCubeMesh.vertices = vertices3;
        restOfCubeMesh.RecalculateBounds();
        restOfCubeMesh.RecalculateNormals();
        meshColliderRestOfCube.sharedMesh = restOfCubeMesh;
        rbRestCube.useGravity = true;
        Destroy(restOfCube, 4f);
    }

    void RecalculateMovingObject(GameObject movingObject, Mesh movingObjMesh, Vector3[] vertices2)
    {
        var meshColliderMovingObj = movingObject.GetComponent<MeshCollider>();
        movingObjMesh.vertices = vertices2;
        movingObjMesh.RecalculateBounds();
        movingObjMesh.RecalculateNormals();
        meshColliderMovingObj.sharedMesh = movingObjMesh;
    }
}
