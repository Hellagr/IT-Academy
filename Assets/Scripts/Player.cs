using UnityEngine;

public static class Tags
{
    public const string PLAYER = "Player";
    public const string GROUND = "Ground";
    public const string ENEMY = "Enemy";
    public const string DEATHBYFALL = "DeathByFall";
    public const string FINISH = "Finish";
}

public class Player : MonoBehaviour
{
    public int lifes = 3;
    public float bottomPoint;
    public bool damageByFall = false;
    public BoxCollider2D boxCollider2D;

    Movement playerMovement;
    Player currentPlayer;
    Vector2 startPos;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerMovement = GetComponent<Movement>();
        startPos = transform.position;
    }

    public float CheckYpos()
    {
        bottomPoint = boxCollider2D.bounds.min.y;
        return bottomPoint;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.FINISH))
        {
            Death();
        }

        if (collision.gameObject.CompareTag(Tags.DEATHBYFALL))
        {
            lifes--;
            damageByFall = true;
            playerMovement.isHitted = true;
            if (lifes > 0)
            {
                TeleportToStart();
            }
        }
    }

    public void DeathCheck()
    {
        if (lifes < 1)
        {
            Death();
        }
    }

    public void Death()
    {
        Time.timeScale = 0f;
        Destroy(gameObject);
    }

    void TeleportToStart()
    {
        transform.position = startPos;
    }
}
