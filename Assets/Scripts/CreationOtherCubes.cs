using Unity.Cinemachine;
using UnityEngine;

public class CreationOtherCubes : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] float randomSpotDistance = 3f;
    [SerializeField] float speed = 1f;
    public GameObject mainCube;
    public GameObject previousObject;
    public GameObject movingObject;
    MainCubeCreateion mainCubeCreation;
    Vector3 lastObjectPos;
    Vector3 directionOfObject;
    float prevPosX;
    float prevPosZ;

    void Start()
    {
        mainCubeCreation = mainCube.GetComponent<MainCubeCreateion>();
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

        cinemachineCamera.Follow = previousObject.transform;
        cinemachineCamera.LookAt = previousObject.transform;

        var prevMesh = previousObject.GetComponent<MeshFilter>().mesh;

        movingObject = Instantiate(previousObject, posForNewObj, Quaternion.identity);
        movingObject.GetComponent<MeshFilter>().mesh = prevMesh;
        movingObject.transform.position = posForNewObj;
    }

    void Update()
    {
        if (movingObject != null)
        {
            movingObject.transform.Translate(directionOfObject * speed * Time.deltaTime, Space.Self);
        }
    }
}
