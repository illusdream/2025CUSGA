using System;
using ilsFramework;

namespace Props
{
    public class GammaGuilderProp : BaseProp
    {
        public override Type ConfigType=>typeof(GammaGuilderPropConfig);
        public override void UseProp(EntityHandler handler)
        {
            if (handler.TryGetComponet(EntityComponetUsage.Health,out BaseHealthComponent health))
            {
                if (health.TryGetHealthSource(EHealthSourceType.Shield,out var source))
                {
                    source.AddValue(100);
                }
            }
        }
    }
}