using UnityEngine;
using UnityEngine.Playables;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    [SerializeField] Animator animator;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float lerpSpeed = 5f;
    [SerializeField] float jumpBlendSpeed = 2f;
    float jumpBlendValue = 1f;
    float prevX;
    float prevY;

    void Update()
    {
        prevX = Mathf.Lerp(prevX, playerMovement.moveInput.x, lerpSpeed * Time.deltaTime);
        prevY = Mathf.Lerp(prevY, playerMovement.moveInput.y, lerpSpeed * Time.deltaTime);

        if (Mathf.Approximately(playerMovement.moveInput.x, 0) && Mathf.Approximately(playerMovement.moveInput.y, 0))
        {
            animator.SetFloat("x", 0f);
            animator.SetFloat("y", 0f);
        }
        else
        {
            animator.SetFloat("x", prevX);
            animator.SetFloat("y", prevY);
        }

        if (playerMovement.isJumping)
        {
            animator.SetBool("isJumping", true);
            jumpBlendValue = Mathf.Lerp(jumpBlendValue, 0f, jumpBlendSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isJumping", false);
            jumpBlendValue = 0f;
        }
        animator.SetFloat("jumpingTree", jumpBlendValue);
    }
}
