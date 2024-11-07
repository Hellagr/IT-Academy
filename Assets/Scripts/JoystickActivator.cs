using UnityEngine;

public class JoystickActivator : MonoBehaviour
{
    public GameObject joystick;
    void Start()
    {
        // Check if in Unity Editor
        if (Application.isEditor)
        {
            Debug.Log("Running in Unity Editor");

            // Check if it's a mobile simulation in the editor (Device Simulator)
            if ((Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) &&
                (Screen.width != 1920 && Screen.height != 1080))  // Check if the resolution is not typical for desktop
            {
                Debug.Log("Likely running in Device Simulator");
                // Handle simulation logic here
            }
            else
            {
                Debug.Log("Running on a desktop simulation in Unity Editor");
                // Handle desktop simulation behavior here
            }
        }
        else
        {
            // Running on a real device
            if (Application.platform == RuntimePlatform.Android)
            {
                Debug.Log("Running on a real Android device");
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Debug.Log("Running on a real iOS device");
            }
            else
            {
                Debug.Log("Running on a real desktop platform");
            }
        }
    }
}
