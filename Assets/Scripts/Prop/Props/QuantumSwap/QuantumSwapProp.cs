using System;

namespace Props
{
    public class QuantumSwapProp : BaseProp
    {
        public override Type ConfigType => typeof(QuantumSwapPropConfig);
        public override void UseProp(EntityHandler handler)
        {
             var p1Transform = CharacterManager.Instance.Player1Controller.transform;
             var p2Transform = CharacterManager.Instance.Player2Controller.transform;
             
             (p1Transform.position, p2Transform.position) = (p2Transform.position, p1Transform.position);
        }
    }
}