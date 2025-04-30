using System;
using UnityEngine;

namespace Props
{
    public class DodgeBallProp : BaseProp,IPropApplyEffect
    {
        public int CanUseCount = 2;
        public override Type ConfigType => typeof(DodgeBallPropConfig);

        public override Type PropStateType => typeof(DodgeBallState);

        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void ApplyEffect(EntityHandler handler)
        {
            handler.transform.position = new Vector3((0,16).RandomRange(), (0,16).RandomRange());
        }
    }
}