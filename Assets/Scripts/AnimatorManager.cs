using UnityEngine;
using System;

public static class AnimatorVariables
{
    public const string SPEEDX = "SpeedX";
    public const string SPEEDY = "SpeedY";
    public const string ISIDLE = "isIdle";
}

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    CharacterMovement characterMovement;
    bool isIdle = true;
    int currentStateX = 0;
    int currentStateY = 0;

    void Awake()
    {
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if (currentStateX != Math.Sign(characterMovement.moveInput.x) || currentStateY != Math.Sign(characterMovement.moveInput.y))
        {
            isIdle = characterMovement.moveInput == Vector2.zero;
            currentStateX = Math.Sign(characterMovement.moveInput.x);
            currentStateY = Math.Sign(characterMovement.moveInput.y);
            SetAnimatorFloat();
        }
    }

    void SetAnimatorFloat()
    {
        animator.SetInteger(AnimatorVariables.SPEEDX, Math.Sign(characterMovement.moveInput.x));
        animator.SetInteger(AnimatorVariables.SPEEDY, Math.Sign(characterMovement.moveInput.y));
        animator.SetBool(AnimatorVariables.ISIDLE, isIdle);
    }
}