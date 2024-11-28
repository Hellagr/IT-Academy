using UnityEngine;
public static class AnimationHashes
{
    public static readonly int WALKTOPLEFT = Animator.StringToHash("WalkTopLeft");
    public static readonly int WALKTOPRIGHT = Animator.StringToHash("WalkTopRight");
    public static readonly int WALKBOTTOMLEFT = Animator.StringToHash("WalkBottomLeft");
    public static readonly int WALKBOTTOMRIGHT = Animator.StringToHash("WalkBottomRight");
    public static readonly int IDLETOPLEFT = Animator.StringToHash("IdleTopLeft");
    public static readonly int IDLETOPRIGHT = Animator.StringToHash("IdleTopRight");
    public static readonly int IDLEBOTTOMLEFT = Animator.StringToHash("IdleBottomLeft");
    public static readonly int IDLEBOTTOMRIGHT = Animator.StringToHash("IdleBottomRight");
}

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    CharacterMovement characterMovement;

    void Awake()
    {
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if (Mathf.Approximately(characterMovement.newVector.x, 2) && Mathf.Approximately(characterMovement.newVector.y, 1))
        {
            animator.SetInteger("SpeedX", 2);
            animator.SetInteger("SpeedY", 1);
        }
        else if (Mathf.Approximately(characterMovement.newVector.x, -2) && Mathf.Approximately(characterMovement.newVector.y, 1))
        {
            animator.SetInteger("SpeedX", -2);
            animator.SetInteger("SpeedY", 1);
        }
        else if (Mathf.Approximately(characterMovement.newVector.x, -2) && Mathf.Approximately(characterMovement.newVector.y, -1))
        {
            animator.SetInteger("SpeedX", -2);
            animator.SetInteger("SpeedY", -1);
        }
        else if (Mathf.Approximately(characterMovement.newVector.x, 2) && Mathf.Approximately(characterMovement.newVector.y, -1))
        {
            animator.SetInteger("SpeedX", 2);
            animator.SetInteger("SpeedY", -1);
        }
        else
        {
            animator.SetInteger("SpeedX", 0);
            animator.SetInteger("SpeedY", 0);
        }
    }
}
