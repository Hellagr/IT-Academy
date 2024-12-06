using Unity.Cinemachine;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    [SerializeField] float randomSpotDistance = 3f;
    [SerializeField] GameObject mainCube;
    [SerializeField] GameManager gameManager;
    public GameObject previousObject { get; set; }
    public GameObject movingObject { get; set; }
    MainCube mainCubeCreation;
    Vector3 lastObjectPos;
    public Vector3 directionOfObject { get; private set; }
    float prevPosX;
    float prevPosZ;

    public void SetPreviousObject(GameObject obj)
    {
        previousObject = obj;
    }

    public void SetMovingObject(GameObject obj)
    {
        movingObject = obj;
    }

    void Start()
    {
        mainCubeCreation = mainCube.GetComponent<MainCube>();
        lastObjectPos = mainCube.transform.position;
        CreateARandomCube();
    }

    public void CreateARandomCube()
    {
        var newHighOfObject = lastObjectPos.y + mainCubeCreation.highOfObject;
        prevPosX = movingObject != null ? movingObject.transform.position.x : mainCube.transform.position.x;
        prevPosZ = movingObject != null ? movingObject.transform.position.z : mainCube.transform.position.z;

        Vector3 posForNewObj = CalculateNewPosition(newHighOfObject);
        CalculateNewRotation(posForNewObj);
        CreateNewObject(posForNewObj);
    }


    public Vector3 CalculateNewPosition(float newHighOfObject)
    {
        Vector3 posForNewObj;

        if (Random.Range(0, 2) == 0)
        {
            posForNewObj = Random.Range(0, 2) == 0 ? new Vector3(randomSpotDistance, newHighOfObject, prevPosZ) : new Vector3(-randomSpotDistance, newHighOfObject, prevPosZ);
        }
        else
        {
            posForNewObj = Random.Range(0, 2) == 0 ? new Vector3(prevPosX, newHighOfObject, randomSpotDistance) : new Vector3(prevPosX, newHighOfObject, -randomSpotDistance);
        }

        lastObjectPos = posForNewObj;
        return posForNewObj;
    }

    private void CalculateNewRotation(Vector3 posForNewObj)
    {
        Vector2 objectPosition = new Vector2(posForNewObj.x, posForNewObj.z);
        Vector2 targetPosition = new Vector2(prevPosX, prevPosZ);
        directionOfObject = new Vector3(targetPosition.x - objectPosition.x, 0f, targetPosition.y - objectPosition.y);
    }

    private void CreateNewObject(Vector3 posForNewObj)
    {
        if (previousObject == null)
        {
            previousObject = mainCube;
        }
        else
        {
            previousObject = movingObject;
        }

        gameManager.SetCameraPosition();

        var prevMesh = previousObject.GetComponent<MeshFilter>().mesh;

        movingObject = Instantiate(previousObject, posForNewObj, Quaternion.identity);
        movingObject.GetComponent<MeshFilter>().mesh = prevMesh;
        movingObject.transform.position = posForNewObj;
    }
}
