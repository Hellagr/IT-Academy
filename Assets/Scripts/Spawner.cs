using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject panelUI;
    [SerializeField] CinemachineCamera virtualCamera;
    public GameObject player;
    Vector2 startPos;

    void Awake()
    {
        startPos = transform.position;
        player = Instantiate(playerPrefab, startPos, Quaternion.identity);

        if (virtualCamera != null && playerPrefab != null)
        {
            virtualCamera.Follow = player.transform;
            virtualCamera.LookAt = player.transform;
        }
    }

    void Update()
    {
        if (player == null)
        {
            LoadUI();
        }
    }

    public void LoadUI()
    {
        panelUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
}