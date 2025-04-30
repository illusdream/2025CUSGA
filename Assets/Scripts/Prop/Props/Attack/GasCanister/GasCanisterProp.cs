using System;

namespace Props
{
    public class GasCanisterProp : BaseProp,IPropApplyEffect
    {
        public override Type ConfigType => typeof(GasCanisterPropConfig);
        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void ApplyEffect(EntityHandler handler)
        {
            if (handler.TryGetComponet(EntityComponetUsage.Buff,out BaseBuffContainer buffContainer))
            {
                buffContainer.AddBuff(EBuffType.SpawnToxicGasBuff);   
            }
        }
    }
}