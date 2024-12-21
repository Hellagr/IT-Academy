using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class CameraPositionName
{
    public const string UP = "Up";
    public const string DOWN = "Down";
    public const string LEFT = "Left";
    public const string FACE = "Face";
}

public abstract class MiniCameraSetter : MonoBehaviour
{
    protected Transform parentWithShipsTransform;
    protected Camera miniCamera;
    protected Button button;
    protected float distanceToShip = 20f;

    protected Dictionary<string, Vector3> cameraPositions;

    protected void Start()
    {
        parentWithShipsTransform = GeneralDataManager.Instance.ParentTransformWithAllShips;
        miniCamera = GeneralDataManager.Instance.MiniCamera;
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeCameraPosition);

        cameraPositions = new Dictionary<string, Vector3>
        {
            { CameraPositionName.UP, new Vector3(0,  distanceToShip, 0) },
            { CameraPositionName.DOWN, new Vector3(0, -distanceToShip, 0) },
            { CameraPositionName.LEFT, new Vector3(-distanceToShip, 0, 0) },
            { CameraPositionName.FACE, new Vector3(0, 0, distanceToShip) },
        };
    }

    protected void ChangeCameraPosition()
    {
        if (cameraPositions.TryGetValue(gameObject.name, out Vector3 vector))
        {
            miniCamera.transform.position = vector;
            miniCamera.transform.LookAt(parentWithShipsTransform);
        }
        else
        {
            Debug.LogError($"This neme of button, {gameObject.name}, doesn't exist!");
        }
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(ChangeCameraPosition);
    }
}
