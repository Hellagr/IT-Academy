using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] CreationOtherCubes creationOtherCubes;
    [SerializeField] GameObject PanelUI;

    public void CutTheMovingObject()
    {
        //GameObject restOfCube = creationOtherCubes.movingObject;

        var preObj = creationOtherCubes.previousObject.GetComponent<MeshFilter>().mesh;
        var movingObj = creationOtherCubes.movingObject.GetComponent<MeshFilter>().mesh;
        //var restOfCubeMesh = restOfCube.GetComponent<MeshFilter>().mesh;

        Vector3[] vertices1 = preObj.vertices;
        Vector3[] vertices2 = movingObj.vertices;
        //Vector3[] vertices3 = restOfCubeMesh.vertices;

        var preObjXmax = creationOtherCubes.previousObject.transform.TransformPoint(preObj.bounds.max).x;
        var preObjXmin = creationOtherCubes.previousObject.transform.TransformPoint(preObj.bounds.min).x;
        var preObjZmax = creationOtherCubes.previousObject.transform.TransformPoint(preObj.bounds.max).z;
        var preObjZmin = creationOtherCubes.previousObject.transform.TransformPoint(preObj.bounds.min).z;

        for (var i = 0; i < vertices2.Length; i++)
        {
            float xVertexInGlobal = creationOtherCubes.movingObject.transform.TransformPoint(vertices2[i]).x;
            float yVertexInGlobal = creationOtherCubes.movingObject.transform.TransformPoint(vertices2[i]).y;
            float zVertexInGlobal = creationOtherCubes.movingObject.transform.TransformPoint(vertices2[i]).z;

            //check x
            if (preObjXmax < xVertexInGlobal)
            {
                Vector3 globalPositionOfCube = new Vector3(preObjXmax, yVertexInGlobal, zVertexInGlobal);
                vertices2[i] = creationOtherCubes.movingObject.transform.InverseTransformPoint(globalPositionOfCube);

            }
            //else
            //{
            //    //make vertecies for rest of cube of X axis (from left)
            //    Vector3 globalPositionOfRest = new Vector3(preObjXmax, yVertexInGlobal, zVertexInGlobal);
            //    vertices3[i] = restOfCube.transform.InverseTransformPoint(globalPositionOfRest);
            //}
            if (preObjXmin > xVertexInGlobal)
            {
                Vector3 globalPositionOfCube = new Vector3(preObjXmin, yVertexInGlobal, zVertexInGlobal);
                vertices2[i] = creationOtherCubes.movingObject.transform.InverseTransformPoint(globalPositionOfCube);
            }
            //else
            //{
            //    //make vertecies for rest of cube of X axis (from right)
            //    Vector3 globalPositionOfRest = new Vector3(preObjXmax, yVertexInGlobal, zVertexInGlobal);
            //    vertices3[i] = restOfCube.transform.InverseTransformPoint(globalPositionOfRest);
            //}

            //check z
            if (preObjZmax < zVertexInGlobal)
            {
                Vector3 globalPositionOfCube = new Vector3(xVertexInGlobal, yVertexInGlobal, preObjZmax);
                vertices2[i] = creationOtherCubes.movingObject.transform.InverseTransformPoint(globalPositionOfCube);
            }
            if (preObjZmin > zVertexInGlobal)
            {
                Vector3 globalPositionOfCube = new Vector3(xVertexInGlobal, yVertexInGlobal, preObjZmin);
                vertices2[i] = creationOtherCubes.movingObject.transform.InverseTransformPoint(globalPositionOfCube);
            }
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

    //void CreateRestOfCube(GameObject restOfCube, Mesh oldMesh, Vector3[] newVertices3)
    //{
    //    GameObject restOfObj = new GameObject("rest", typeof(MeshFilter), typeof(MeshRenderer));
    //    restOfObj.transform.position = new Vector3(restOfCube.transform.position.x - 5f, restOfCube.transform.position.y, restOfCube.transform.position.z - 5f);
    //    var restMesh = restOfObj.GetComponent<MeshFilter>().mesh;
    //    restMesh.vertices = newVertices3;
    //    restOfCube.AddComponent<Rigidbody>();
    //}

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
}
