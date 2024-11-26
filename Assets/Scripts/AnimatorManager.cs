using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class AnimationHashes
{
    public static readonly int WALK = Animator.StringToHash("Walk");
    public static readonly int JUMP = Animator.StringToHash("Jump");
    public static readonly int DEATH = Animator.StringToHash("Death");
    public static readonly int HIT = Animator.StringToHash("Hit");
}

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] Spawner playerOnScene;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemy2;
    [SerializeField] RectTransform lifes;
    Movement playerMovement;
    Player playerComponent;
    Enemy enemyComponent;
    Enemy enemy2Component;
    Animator playerAnimator;
    Animator enemyAnimator;
    Animator enemy2Animator;
    float currentHP;

    void Start()
    {
        playerComponent = playerOnScene.player.GetComponent<Player>();
        playerMovement = playerOnScene.player.GetComponent<Movement>();
        playerAnimator = playerOnScene.player.GetComponent<Animator>();

        enemyAnimator = enemy.GetComponent<Animator>();
        enemyComponent = enemy.GetComponent<Enemy>();

        enemy2Animator = enemy2.GetComponent<Animator>();
        enemy2Component = enemy2.GetComponent<Enemy>();

        currentHP = playerComponent.lifes;
    }

    void Update()
    {
        if (playerAnimator != null)
        {
            AnimatePlayer();
        }

        AnimateEnemy();
    }

    void AnimatePlayer()
    {
        playerAnimator.SetInteger(AnimationHashes.WALK, playerMovement.moveInput.x != 0 ? 1 : 0);

        if (currentHP > playerComponent.lifes)
        {
            playerAnimator.SetTrigger(AnimationHashes.HIT);

            int imageLifes = lifes.transform.childCount;
            if (imageLifes > 0)
            {
                Transform life = lifes.transform.GetChild(imageLifes - 1);
                Destroy(life.gameObject);
            }

            currentHP--;
        }

        if (playerMovement.isJumping)
        {
            playerAnimator.SetBool(AnimationHashes.JUMP, true);
        }
        else
        {
            playerAnimator.SetBool(AnimationHashes.JUMP, false);
        }
    }

    void AnimateEnemy()
    {
        if (enemyAnimator != null)
        {
            enemyAnimator.SetInteger(AnimationHashes.WALK, enemyComponent.velocity != 0.0f ? 1 : 0);
            if (!enemyComponent.isLive)
            {
                enemyAnimator.SetTrigger(AnimationHashes.DEATH);
            }
        }

        if (enemy2Animator != null)
        {
            enemy2Animator.SetInteger(AnimationHashes.WALK, enemy2Component.velocity != 0.0f ? 1 : 0);
            if (!enemy2Component.isLive)
            {
                enemy2Animator.SetTrigger(AnimationHashes.DEATH);
            }
        }
    }
}
