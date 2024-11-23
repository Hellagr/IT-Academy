using TMPro;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{
    //public static MoveSprite Instance { get; private set; }

    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject spritePrefab;
    [SerializeField] float speedOfSprite = 1f;
    Transform backGround;
    SpriteRenderer spriteRenderer;
    float screenWidth;
    float halfScreenWidth;
    float extendScreenWidth;
    float spriteWidth;
    float halfOfSpriteWidth;
    float necessaryAmountOfSpritesForRightSide;
    float centerOfRightExtremeSprite = 0f;
    float aspectRatio;
    float cameraOrtographicSize;

    public bool isRight = true;

    private void Start()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        backGround = transform;

        aspectRatio = (float)Screen.width / Screen.height;
        cameraOrtographicSize = mainCamera.orthographicSize;
        screenWidth = 2 * cameraOrtographicSize * aspectRatio;
        halfScreenWidth = screenWidth / 2;
        extendScreenWidth = halfScreenWidth + screenWidth;

        spriteRenderer = spritePrefab.transform.GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.size.x;
        halfOfSpriteWidth = spriteWidth / 2;
        necessaryAmountOfSpritesForRightSide = Mathf.Ceil((halfScreenWidth - halfOfSpriteWidth + screenWidth) / screenWidth);

        centerOfRightExtremeSprite = spriteWidth;
        Instantiate(spritePrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);

        for (int i = 0; i < necessaryAmountOfSpritesForRightSide; i++)
        {
            Instantiate(spritePrefab, new Vector3(centerOfRightExtremeSprite, 0, 0), Quaternion.identity, transform);
            Instantiate(spritePrefab, new Vector3(-centerOfRightExtremeSprite, 0, 0), Quaternion.identity, transform);
            centerOfRightExtremeSprite += spriteWidth;
        }
    }

    void Update()
    {
        if (backGround.position.x >= spriteWidth || backGround.position.x <= -spriteWidth)
        {
            backGround.position = new Vector3(0, 0, 0);
        }

        float direction = isRight ? -speedOfSprite : speedOfSprite;

        backGround.position = new Vector3(backGround.position.x + direction, 0, 0);
    }
}
