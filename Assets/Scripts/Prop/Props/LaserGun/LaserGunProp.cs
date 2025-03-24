using System;

namespace Props
{
    public class LaserGunProp : BaseProp
    {
        public override Type ConfigType => typeof(LaserGunPropConfig);

        LaserGunPropConfig config;
        
        public override void Initialize(BasePropConfig config)
        {
            if (config is LaserGunPropConfig LGPC)
            {
                this.config = LGPC;
            }
            base.Initialize(config);
        }

        public override void UseProp(EntityHandler handler)
        {
            //生成预制体
        }
    }
}