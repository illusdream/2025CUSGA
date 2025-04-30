using DefaultNamespace;
using ilsFramework;
using UnityEngine;

public class InBlackHoleBuff : BaseBuff<InBlackHoleBuffConfig>
{
    public override EBuffTag BuffTag => EBuffTag.UnRemoveAbleControl;

    protected override void OnAddBuff(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.playerController,out PlayerController controller))
        {
            controller.TryInToBlackHole();
        }


    }

    protected override void OnBuffTick(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove moveable))
        {
            moveable.SetTargetVelocity(Vector3.zero);
        }
    }

    protected override void OnRemoveBuff(EntityHandler handler)
    {
       
    }

    public override void OnResetBuffTimer()
    {
        
    }
}