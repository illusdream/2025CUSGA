using DefaultNamespace;

public class BlurringBuff : BaseBuff<BlurringBuffConfig>
{
    protected override void OnAddBuff(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.Buff,out BaseBuffContainer buffContainer))
        {
            var allControl = BuffManager.Instance.GetAllBuffTypeBySameTag(EBuffTag.Control);

            foreach (var type in allControl)
            {
                buffContainer.RemoveBuff(type);
            }
            buffContainer.IgnoreBuffTag |= EBuffTag.Control | EBuffTag.UnRemoveAbleControl;
        }
        
        
        NumericModifier acceleration = new NumericModifier(additive:Config.AccelerationAddRate);
        
        NumericModifier maxSpeed = new NumericModifier(additive:Config.MaxSpeedAddRate);
        
        if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove move))
        {
            move.AccelerationModifiers.Add("BlurringBuff", acceleration);
            move.MaxMoveSpeedModifiers.Add("BlurringBuff", maxSpeed);
        }
        
        
        if (handler.TryGetComponet(EntityComponetUsage.Hitable,out BaseHitable hitable))
        {
            hitable._canBeHit = false;
        }
    }

    protected override void OnBuffTick(EntityHandler handler)
    {
        
    }

    protected override void OnRemoveBuff(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.Buff,out BaseBuffContainer buffContainer))
        {
            buffContainer.IgnoreBuffTag &= (~EBuffTag.Control);
            buffContainer.IgnoreBuffTag &= (~EBuffTag.UnRemoveAbleControl);
        }
        
        if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove move))
        {
            move.AccelerationModifiers.Remove("BlurringBuff");
            move.MaxMoveSpeedModifiers.Remove("BlurringBuff");
        }
        
        if (handler.TryGetComponet(EntityComponetUsage.Hitable,out BaseHitable hitable))
        {
            hitable._canBeHit = true;
        }
    }

    public override void OnResetBuffTimer()
    {
        
    }
}