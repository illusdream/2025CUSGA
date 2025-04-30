using System;
using AreaInfos.Shapes;
using UnityEngine;

namespace Props
{
    public class RCBmobsControllerProp : BaseProp,IPropApplyEffect
    {
        public override Type ConfigType => typeof(RCBmobsControllerPropConfig);
        
        public RCBmobsGOController GOController;
        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void ApplyEffect(EntityHandler handler)
        {
            GOController.ImmediateToBomb();
        }
    }
}