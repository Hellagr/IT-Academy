using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] CreationOtherCubes creationOtherCubes;
    [SerializeField] GameObject PanelUI;

    public void CutTheMovingObject()
    {
        GameObject restOfCube = creationOtherCubes.movingObject;

        var preObj = creationOtherCubes.previousObject.GetComponent<MeshFilter>().mesh;
        var movingObj = creationOtherCubes.movingObject.GetComponent<MeshFilter>().mesh;
        var restOfCubeMesh = restOfCube.GetComponent<MeshFilter>().mesh;

        Vector3[] vertices1 = preObj.vertices;
        Vector3[] vertices2 = movingObj.vertices;
        Vector3[] vertices3 = restOfCubeMesh.vertices;

        var preObjXmax = creationOtherCubes.previousObject.transform.TransformPoint(preObj.bounds.max).x;
        var preObjXmin = creationOtherCubes.previousObject.transform.TransformPoint(preObj.bounds.min).x;
        var preObjZmax = creationOtherCubes.previousObject.transform.TransformPoint(preObj.bounds.max).z;
        var preObjZmin = creationOtherCubes.previousObject.transform.TransformPoint(preObj.bounds.min).z;

        for (var i = 0; i < vertices2.Length; i++)
        {
            Vector3 vertexInGlobal = creationOtherCubes.movingObject.transform.TransformPoint(vertices2[i]);
            float xVertexInGlobal = vertexInGlobal.x;
            float yVertexInGlobal = vertexInGlobal.y;
            float zVertexInGlobal = vertexInGlobal.z;

            CheckXaxis(vertices2, vertices3, preObjXmax, preObjXmin, i, xVertexInGlobal, yVertexInGlobal, zVertexInGlobal);
            CheckZaxis(vertices2, vertices3, preObjZmax, preObjZmin, i, xVertexInGlobal, yVertexInGlobal, zVertexInGlobal);
        }

        //CreateRestOfCube(restOfCube, restOfCubeMesh, vertices3);

        movingObj.vertices = vertices2;
        movingObj.RecalculateBounds();
        movingObj.RecalculateNormals();

        CheckIfPlayerLose(movingObj.bounds.max.x, movingObj.bounds.min.x, movingObj.bounds.max.z, movingObj.bounds.min.z, creationOtherCubes);

        if (creationOtherCubes.previousObject != null && creationOtherCubes.movingObject != null)
        {
            creationOtherCubes.CreateARandomCube();
        }
    }

    void CheckXaxis(Vector3[] vertices2, Vector3[] vertices3, float preObjXmax, float preObjXmin, int i, float xVertexInGlobal, float yVertexInGlobal, float zVertexInGlobal)
    {
        if (xVertexInGlobal < preObjXmin || xVertexInGlobal > preObjXmax)
        {
            float xPosition = (xVertexInGlobal < preObjXmin) ? preObjXmin : preObjXmax;
            Vector3 globalPosition = new Vector3(xPosition, yVertexInGlobal, zVertexInGlobal);
            vertices2[i] = creationOtherCubes.movingObject.transform.InverseTransformPoint(globalPosition);
        }

        //rest
        if (xVertexInGlobal < preObjXmin || xVertexInGlobal > preObjXmax)
        {
            float xPosition = (xVertexInGlobal < preObjXmin) ? preObjXmin : preObjXmax;
            Vector3 globalPositionOfRest = new Vector3(xPosition, yVertexInGlobal, zVertexInGlobal);
            vertices3[i] = creationOtherCubes.movingObject.transform.InverseTransformPoint(globalPositionOfRest);
        }
    }

    void CheckZaxis(Vector3[] vertices2, Vector3[] vertices3, float preObjZmax, float preObjZmin, int i, float xVertexInGlobal, float yVertexInGlobal, float zVertexInGlobal)
    {
        if (zVertexInGlobal < preObjZmin || zVertexInGlobal > preObjZmax)
        {
            float zPosition = (zVertexInGlobal < preObjZmin) ? preObjZmin : preObjZmax;
            Vector3 globalPositionOfCube = new Vector3(xVertexInGlobal, yVertexInGlobal, zPosition);
            vertices2[i] = creationOtherCubes.movingObject.transform.InverseTransformPoint(globalPositionOfCube);
        }

        //rest
        if (zVertexInGlobal < preObjZmin || zVertexInGlobal > preObjZmax)
        {
            float zPosition = (zVertexInGlobal < preObjZmin) ? preObjZmin : preObjZmax;
            Vector3 globalPositionOfRest = new Vector3(xVertexInGlobal, yVertexInGlobal, zPosition);
            vertices3[i] = creationOtherCubes.movingObject.transform.InverseTransformPoint(globalPositionOfRest);
        }
    }

    void CheckIfPlayerLose(float xMax, float xMin, float zMax, float zMin, CreationOtherCubes creationOtherCubes)
    {
        if (Mathf.Approximately(xMax, xMin) || Mathf.Approximately(zMax, zMin))
        {
            this.enabled = false;
            PanelUI.SetActive(true);
            Destroy(creationOtherCubes.previousObject);
            Destroy(creationOtherCubes.movingObject);

            Time.timeScale = 0f;
        }
    }

    void CreateRestOfCube(GameObject restOfCube, Mesh oldMesh, Vector3[] newVertices3)
    {
        GameObject restOfObj = new GameObject("rest", typeof(MeshFilter), typeof(MeshRenderer));
        restOfObj.transform.position = new Vector3(restOfCube.transform.position.x - 5f, restOfCube.transform.position.y, restOfCube.transform.position.z - 5f);
        var restMesh = restOfObj.GetComponent<MeshFilter>().mesh;
        restMesh.vertices = newVertices3;
        restOfCube.AddComponent<Rigidbody>();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
}
