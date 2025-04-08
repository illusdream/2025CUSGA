using System;
using ilsFramework;

namespace Props
{
    public class GammaGuilderProp : BaseProp
    {
        public override Type ConfigType=>typeof(GammaGuilderPropConfig);
        GammaGuilderPropConfig config;
        public override void Initialize(BasePropConfig config)
        {
            this.config = (GammaGuilderPropConfig)config;
            base.Initialize(config);
        }

        public override void UseProp(EntityHandler handler)
        {
            if (handler.TryGetComponet(EntityComponetUsage.Health,out BaseHealthComponent health))
            {
                if (health.TryGetHealthSource(EHealthSourceType.Shield,out var source))
                {
                    source.AddValue(config.GuilderValue);
                }
            }
        }
    }
}