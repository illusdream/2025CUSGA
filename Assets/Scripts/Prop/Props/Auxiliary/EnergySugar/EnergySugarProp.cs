using System;

namespace Props
{
    public class EnergySugarProp : BaseProp,IPropApplyEffect
    {
        public override Type ConfigType => typeof(EnergySugarPropConfig);
        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void ApplyEffect(EntityHandler handler)
        {
            if (handler.TryGetComponet(EntityComponetUsage.EnergyContainer,out PlayerEnergyContainer playerEnergyContainer))
            {
                NumericModifier energyModifier = new NumericModifier(additive:((EnergySugarPropConfig)config).EnergyImprove);
                playerEnergyContainer.EnergyModifiers.Add($"EnergySugarBuff{DateTime.Now.Millisecond}",energyModifier);
            }
        }
    }
}