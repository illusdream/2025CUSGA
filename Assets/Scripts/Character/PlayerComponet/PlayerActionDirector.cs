using UnityEngine;

public class PlayerActionDirector : BaseActionDirector
{
    public Animator animator;
    RuntimeAnimatorController animatorController;
    public override void Start()
    {
        onStarted += OnonStarted;
        onStopped += OnonStopped;
        base.Start();
    }

    private void OnonStopped(BaseActionDirector obj)
    {
        
        animator.runtimeAnimatorController = animatorController;
    }

    private void OnonStarted(BaseActionDirector obj)
    {
        animatorController = animator.runtimeAnimatorController;
        animator.runtimeAnimatorController = null;
    }

    public override bool CanPlay()
    {
        return !isPlaying;
    }
}