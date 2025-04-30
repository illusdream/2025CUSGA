using DefaultNamespace;
using UnityEngine;

public class PoisoningBuff : BaseBuff<PoisoningBuffConfig>
{
    public override EBuffTag BuffTag => EBuffTag.Control;

    protected override void OnAddBuff(EntityHandler handler)
    {
        NumericModifier acceleration = new NumericModifier(additive:Config.ReduceAcceleration);
        
        NumericModifier maxSpeed = new NumericModifier(additive:Config.ReduceSpeed);
        
        if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove move))
        {
            move.AccelerationModifiers.Add("PoisoningBuff", acceleration);
            move.MaxMoveSpeedModifiers.Add("PoisoningBuff", maxSpeed);
        }
    }

    protected override void OnBuffTick(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.Hitable,out BaseHitable hitable))
        {
            DamageInfo newInfo = DamageInfo.BuildDamageInfoBySystem(Config.DamagePerSecond * Time.deltaTime);
            hitable.Hit(newInfo,out var info);
        }
    }

    protected override void OnRemoveBuff(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove move))
        {
            move.AccelerationModifiers.Remove("PoisoningBuff");
            move.MaxMoveSpeedModifiers.Remove("PoisoningBuff");
        }
    }

    public override void OnResetBuffTimer()
    {
        
    }
}