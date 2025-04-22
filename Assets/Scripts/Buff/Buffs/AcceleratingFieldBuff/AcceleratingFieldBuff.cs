public class AcceleratingFieldBuff : BaseBuff<AcceleratingFieldBuffConfig>
{
    protected override void OnAddBuff(EntityHandler handler)
    {
        NumericModifier acceleration = new NumericModifier(additive:Config.AccelerationAddRate);
        
        NumericModifier maxSpeed = new NumericModifier(additive:Config.MaxSpeedAddRate);
        
        if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove move))
        {
            move.AccelerationModifiers.Add("AcceleratingField", acceleration);
            move.MaxMoveSpeedModifiers.Add("AcceleratingField", maxSpeed);
        }
    }

    protected override void OnBuffTick(EntityHandler handler)
    {
        
    }

    protected override void OnRemoveBuff(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove move))
        {
            move.AccelerationModifiers.Remove("AcceleratingField");
            move.MaxMoveSpeedModifiers.Remove("AcceleratingField");
        }
    }

    public override void OnResetBuffTimer()
    {
        
    }
}