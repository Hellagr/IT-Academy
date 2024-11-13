using UnityEditor;
using UnityEngine;

public class LiveHandler : MonoBehaviour
{
    Animator animator;
    bool spawn = true;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName("Spawn"))
        {
            Movement.Instance.inputSystemActions.Enable();
            Movement.Instance.animator.ResetTrigger("Landing");
        }
    }
}
