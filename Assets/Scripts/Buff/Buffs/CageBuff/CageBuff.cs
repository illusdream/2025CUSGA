using ilsFramework;

public class CageBuff : BaseBuff<CageBuffConfig>
{
    public override EBuffTag BuffTag => EBuffTag.Control;

    protected override void OnAddBuff(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.playerController,out  PlayerController controller))
        {
            controller.SetCanMove(false);
        }
    }

    protected override void OnBuffTick(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.playerController,out  PlayerController controller))
        {
            controller.SetCanMove(false);
        }
    }

    protected override void OnRemoveBuff(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.playerController,out  PlayerController controller))
        {
            controller.SetCanMove(true);
        }
    }

    public override void OnResetBuffTimer()
    {
        
    }
}