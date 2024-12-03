using UnityEngine;

public class CreationOtherCubes : MonoBehaviour
{
    [SerializeField] float randomSpotDistance = 3f;
    [SerializeField] GameObject mainCube;
    GameObject previousObject;
    GameObject movingObject;
    MainCubeCreateion mainCubeCreation;
    Vector3 lastObjectPos;
    Vector3 directionOfObject;

    void Start()
    {
        mainCubeCreation = mainCube.GetComponent<MainCubeCreateion>();
        lastObjectPos = mainCube.transform.position;
        previousObject = mainCube;
        CreateARandomCube();
    }

    public void CreateARandomCube()
    {
        var newHighOfObject = lastObjectPos.y + mainCubeCreation.highOfObject;

        Vector3 posForNewObj = CalculateNewPosition(newHighOfObject);
        CalculateNewRotation(posForNewObj);
        CreateNewObject(posForNewObj);
    }


    public Vector3 CalculateNewPosition(float newHighOfObject)
    {
        Vector3 posForNewObj;

        float prevPosX = previousObject != null ? previousObject.transform.position.x : mainCube.transform.position.x;
        float prevPosZ = previousObject != null ? previousObject.transform.position.z : mainCube.transform.position.z;

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
        float prevPosX = previousObject != null ? previousObject.transform.position.x : mainCube.transform.position.x;
        float prevPosZ = previousObject != null ? previousObject.transform.position.z : mainCube.transform.position.z;

        Vector2 objectPosition = new Vector2(posForNewObj.x, posForNewObj.z);
        Vector2 targetPosition = new Vector2(prevPosX, prevPosZ);
        directionOfObject = new Vector3(targetPosition.x - objectPosition.x, 0f, targetPosition.y - objectPosition.y);
    }

    private void CreateNewObject(Vector3 posForNewObj)
    {
        movingObject = Instantiate(previousObject, posForNewObj, Quaternion.identity);
        movingObject.transform.position = posForNewObj;
        previousObject = movingObject;
    }

    void Update()
    {
        if (movingObject != null)
        {
            movingObject.transform.Translate(directionOfObject * 0.005f, Space.Self);
        }
    }
}
