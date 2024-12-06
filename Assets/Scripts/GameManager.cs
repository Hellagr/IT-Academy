using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] CubeGenerator cubeGenerator;
    [SerializeField] GameObject PanelUI;
    [SerializeField] TMP_Text result;
    [SerializeField] TMP_Text onlineResult;
    [SerializeField] float timeToLose = 4f;
    private float timeHasPassed = 0f;
    CubeDivider cubeDivider;
    int score = 0;

    void Start()
    {
        cubeDivider = GetComponent<CubeDivider>();
    }

    public void ResetTimer()
    {
        timeHasPassed = 0f;
    }

    public void CheckIfPlayerLose(GameObject movingObject, float xMax, float xMin, float zMax, float zMin)
    {
        if (Mathf.Approximately(xMax, xMin) || Mathf.Approximately(zMax, zMin))
        {
            this.enabled = false;
            PanelUI.SetActive(true);
            Destroy(movingObject);

            Time.timeScale = 0f;
        }

        if (this.enabled)
        {
            score++;
            onlineResult.text = $"Points: {score}";
            cubeGenerator.CreateARandomCube();
        }
        else
        {
            result.text =
            "You've got\n" +
            $"{score}\n" +
            "points";

            onlineResult.enabled = false;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void SetCameraPosition()
    {
        cinemachineCamera.Follow = cubeGenerator.previousObject.transform;
        cinemachineCamera.LookAt = cubeGenerator.previousObject.transform;
    }

    void Update()
    {
        timeHasPassed += Time.deltaTime;
        if (timeHasPassed > timeToLose)
        {
            cubeDivider.CutTheMovingObject();
        }
    }
}
