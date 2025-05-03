

public class FullSpeedAheadBuff : BaseBuff<FullSpeedAheadBuffConfig>
{
    protected override void OnAddBuff(EntityHandler handler)
    {
        NumericModifier acceleration = new NumericModifier(additive:Config.SpeedAddPercent);
        
        NumericModifier maxSpeed = new NumericModifier(additive:Config.AccelerationAddPercent);
        
        if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove move))
        {
            move.AccelerationModifiers.Add("FullSpeedAheadBuff", acceleration);
            move.MaxMoveSpeedModifiers.Add("FullSpeedAheadBuff", maxSpeed);
        }
    }

    protected override void OnBuffTick(EntityHandler handler)
    {
        
    }

    protected override void OnRemoveBuff(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove move))
        {
            move.AccelerationModifiers.Remove("FullSpeedAheadBuff");
            move.MaxMoveSpeedModifiers.Remove("FullSpeedAheadBuff");
        }
    }

    public override void OnResetBuffTimer()
    {
        
    }
}