using System;
using ilsFramework;
using Unity.Mathematics;

namespace Props
{
    public class CageProp : BaseProp,IPropApplyEffect
    {
        public override Type ConfigType => typeof(CagePropConfig);
        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void ApplyEffect(EntityHandler handler)
        {
            if (handler.TryGetComponet(EntityComponetUsage.playerController,out  PlayerController controller))
            {
                switch (controller.PlayerID)
                {
                    case 1:
                    {
                        if (CharacterManager.Instance.Player2Controller.handler.TryGetComponet(EntityComponetUsage.Buff,out BaseBuffContainer container))
                        {
                            container.AddBuff(EBuffType.CageBuff);
                            if (config is CagePropConfig _config)
                            {
                                var result = Entity.Instantiate(_config.CageVisual, SpawnSource.SystemGenerate, container.transform.position, quaternion.identity);
                                result.transform.parent = container.transform;
                            }
                        }
                    }
                        break;
                    case 2:
                    {
                        if (CharacterManager.Instance.Player1Controller.handler.TryGetComponet(EntityComponetUsage.Buff,out BaseBuffContainer container))
                        {
                            container.AddBuff(EBuffType.CageBuff);
                            if (config is CagePropConfig _config)
                            {
                                var result = Entity.Instantiate(_config.CageVisual, SpawnSource.SystemGenerate, container.transform.position, quaternion.identity);
                                result.transform.parent = container.transform;
                            }
                        }
                    }
                        break;
                }
            }
        }
    }
}