using UnityEngine;
using UnityEngine.AI;

public static class AnimationHashes
{
    public static readonly int IS_IDLE = Animator.StringToHash("isIdle");
    public static readonly int JUMP = Animator.StringToHash("Jump");
}

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    bool isJumping = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.velocity.sqrMagnitude > 0.01f && !isJumping)
        {
            animator.SetBool(AnimationHashes.IS_IDLE, false);
        }
        else
        {
            animator.SetBool(AnimationHashes.IS_IDLE, true);
        }

        if (agent.isOnOffMeshLink)
        {
            if (!isJumping)
            {
                animator.SetTrigger(AnimationHashes.JUMP);
            }
            agent.speed = 4f;
            isJumping = true;
        }
        else if (!agent.isOnOffMeshLink && isJumping)
        {
            isJumping = false;
            agent.speed = 7f;
        }
    }
}
