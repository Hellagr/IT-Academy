using UnityEngine;

public class CreationAndMovingSprites : MonoBehaviour
{
    [SerializeField] CharacterController changeDirection;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject spritePrefab;
    [SerializeField] float speedOfSprite = 1f;
    float screenWidth;
    float halfScreenWidth;
    float extendScreenWidth;
    float spriteWidth;
    float halfOfSpriteWidth;
    float necessaryAmountOfSpritesForRightSide;
    float centerOfRightExtremeSprite = 0f;
    float aspectRatio;
    float cameraOrtographicSize;

    private void Start()
    {
        CalculateScreenSize();
        CalculateSpriteSize();
        CreateNecessaryAmountOfSprites();
    }

    private void CalculateScreenSize()
    {
        aspectRatio = (float)Screen.width / Screen.height;
        cameraOrtographicSize = mainCamera.orthographicSize;
        screenWidth = 2 * cameraOrtographicSize * aspectRatio;
        halfScreenWidth = screenWidth / 2;
    }

    private void CalculateSpriteSize()
    {
        var spriteRendererOfObject = spritePrefab.GetComponent<SpriteRenderer>();
        spriteWidth = spriteRendererOfObject.size.x * 1.5f;
        halfOfSpriteWidth = spriteWidth / 2;
        necessaryAmountOfSpritesForRightSide = Mathf.Ceil((halfScreenWidth - halfOfSpriteWidth + screenWidth) / screenWidth);
        centerOfRightExtremeSprite = spriteWidth;
    }
    private void CreateNecessaryAmountOfSprites()
    {
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
        if (transform.position.x >= spriteWidth || transform.position.x <= -spriteWidth)
        {
            transform.position = new Vector3(0, 0, 0);
        }

        float speed = changeDirection.isRight ? -speedOfSprite : speedOfSprite;

        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, 0, 0);
    }
}
