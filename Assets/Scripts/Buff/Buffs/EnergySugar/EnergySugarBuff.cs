using System;
using ilsFramework;

public class EnergySugarBuff : BaseBuff<EnergySugarBuffConfig>
{

    private NumericModifier EnergyModifier = new NumericModifier(additive:1.25f);
    
    protected override void OnAddBuff(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.EnergyContainer,out PlayerEnergyContainer playerEnergyContainer))
        {
            playerEnergyContainer.EnergyModifiers.Add("EnergySugarBuff",EnergyModifier);
        }
    }

    protected override void OnBuffTick(EntityHandler handler)
    {

    }

    protected override void OnRemoveBuff(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.EnergyContainer,out PlayerEnergyContainer playerEnergyContainer))
        {
            playerEnergyContainer.EnergyModifiers.Remove("EnergySugarBuff");
        }
    }

    public override void OnResetBuffTimer()
    {
        
    }
}