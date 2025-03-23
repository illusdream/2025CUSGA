using System;

namespace Props
{
    public class LaserGunProp : BaseProp
    {
        public override Type ConfigType => typeof(LaserGunPropConfig);

        public override void UseProp(EntityHandler handler)
        {
            
        }
    }
}