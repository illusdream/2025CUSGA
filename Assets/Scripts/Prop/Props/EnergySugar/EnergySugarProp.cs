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
            if (handler.TryGetComponet(EntityComponetUsage.Buff,out BaseBuffContainer buffContainer))
            {
                buffContainer.AddBuff(EBuffType.EnergySugarBuff);   
            }
        }
    }
}