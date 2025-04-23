using DefaultNamespace;
using ilsFramework;

public class InBlackHoleBuff : BaseBuff<InBlackHoleBuffConfig>
{
    protected override void OnAddBuff(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.playerController,out PlayerController controller))
        {
            controller.TryInToBlackHole();
        }
    }

    protected override void OnBuffTick(EntityHandler handler)
    {
        
    }

    protected override void OnRemoveBuff(EntityHandler handler)
    {
       
    }

    public override void OnResetBuffTimer()
    {
        
    }
}