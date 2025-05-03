public class TotalMayhemBuff : BaseBuff<TotalMayhemBuffConfig>
{
    protected override void OnAddBuff(EntityHandler handler)
    {
        NumericModifier EnergyModifier = new NumericModifier(additive:Config.EnergyAddPercent);
        
        if (handler.TryGetComponet(EntityComponetUsage.EnergyContainer,out PlayerEnergyContainer playerEnergyContainer))
        {
            playerEnergyContainer.EnergyModifiers.Add("TotalMayhemBuff",EnergyModifier);
        }
    }

    protected override void OnBuffTick(EntityHandler handler)
    {
           
    }

    protected override void OnRemoveBuff(EntityHandler handler)
    {
        if (handler.TryGetComponet(EntityComponetUsage.EnergyContainer,out PlayerEnergyContainer playerEnergyContainer))
        {
            playerEnergyContainer.EnergyModifiers.Remove("TotalMayhemBuff");
        }
    }

    public override void OnResetBuffTimer()
    {
          
    }
}