using UnityEngine;

public class PlayAnimationAction : BaseAction
{
    public string animationName; // 要播放的动画名称

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator component is missing.");
        }
    }

    public override void Execute()
    {
        if (animator != null && !string.IsNullOrEmpty(animationName))
        {
            animator.Play(animationName);
        }
        else
        {
            Debug.LogWarning("Animator or animationName is missing.");
        }
    }
}