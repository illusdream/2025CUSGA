using UnityEngine;

public class PlayerActionDirector : BaseActionDirector
{
    public Animator animator;
    RuntimeAnimatorController animatorController;

    public bool CanSkip = false;
    public override void Start()
    {
        onStarted += OnonStarted;
        onStopped += OnonStopped;
        base.Start();
    }

    private void OnonStopped(BaseActionDirector obj)
    {
        
        animator.runtimeAnimatorController = animatorController;
        CanSkip = false;
    }

    private void OnonStarted(BaseActionDirector obj)
    {
        animatorController = animator.runtimeAnimatorController;
        animator.runtimeAnimatorController = null;
    }

    public override bool CanPlay()
    {
        return !isPlaying || CanSkip;
    }
}