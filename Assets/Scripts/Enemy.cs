using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Spawner playerSpawnedOnScene;
    [SerializeField] float repulseForce = 35f;
    [SerializeField] float period = 3f;
    public float velocity = 4f;
    public bool isLive = true;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    Movement playerMovement;
    Player playerComponent;
    Rigidbody2D playerRB;
    Rigidbody2D enemyRB;
    Vector2 offset;
    Vector2 amplitude;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyRB = GetComponent<Rigidbody2D>();
        playerRB = playerSpawnedOnScene.player.GetComponent<Rigidbody2D>();
        playerMovement = playerSpawnedOnScene.player.GetComponent<Movement>();
        playerComponent = playerSpawnedOnScene.player.GetComponent<Player>();
    }

    void Update()
    {
        EnemyMoving();
    }

    void EnemyMoving()
    {
        float cycle = Time.time / period;
        const float tau = 2 * Mathf.PI;
        float rawSinValue = Mathf.Sin(cycle * tau);
        float normalizedValue = (rawSinValue + 1) / 2;

        float desiredDirection;

        if (normalizedValue >= 0.5)
        {
            desiredDirection = velocity;
            spriteRenderer.flipX = true;
        }
        else
        {
            desiredDirection = -velocity;
            spriteRenderer.flipX = false;
        }

        if (isLive)
        {
            enemyRB.linearVelocity = new Vector2(desiredDirection, enemyRB.linearVelocityY);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.PLAYER))
        {
            var playerBottomPoint = playerComponent.CheckYpos();
            var enemyTopPoint = boxCollider2D.bounds.max.y;
            if (playerBottomPoint >= enemyTopPoint)
            {
                playerRB.AddForce(Vector2.up * repulseForce, ForceMode2D.Impulse);
                isLive = false;
            }
            else
            {
                var playerPos = playerComponent.transform.position;
                Vector2 repusleVector = playerPos.x > transform.position.x ? Vector2.right : Vector2.left;
                playerMovement.isHitted = true;
                playerRB.AddForce(repusleVector * repulseForce, ForceMode2D.Impulse);
                playerComponent.lifes -= 1;
            }
        }
    }

    public void DeathEvent()
    {
        Destroy(gameObject);
    }
}
