using System;

namespace Props
{
    public class ReflectingPrismProp : BaseProp
    {
        public override Type ConfigType => typeof(ReflectingPrismPropConfig);
        public override void UseProp(EntityHandler handler)
        {
            throw new NotImplementedException();
        }
    }
}