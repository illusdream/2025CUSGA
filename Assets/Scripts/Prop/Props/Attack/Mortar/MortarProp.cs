using System;
using AreaInfos.Shapes;
using UnityEngine;

namespace Props
{
    public class MortarProp : BaseProp,IPropSpawnEntity
    {
        public override Type ConfigType => typeof(MortarPropConfig);

        public override Type PropStateType => typeof(MortarPropState);

        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void SpawnEntity(PointShape pointShape, EntityHandler entityHandler, Transform pivotTransform)
        {
            //生成Aim
            var config = (MortarPropConfig)this.config;
            pointShape.GetCurrentData(pivotTransform,out var point);
            var result =  Entity.Instantiate(config.mortarAimPrefab, entityHandler.SpawnEntityBySelf(),point,Quaternion.identity);

            
            var controllerInstance = PropManager.Instance.CreateTargetProp(typeof(MortarControllerProp)) as MortarControllerProp;
            if (result.TryGetComponent<MortarAimController>(out var aimController) && entityHandler.TryGetComponet(EntityComponetUsage.playerController, out PlayerController playerController))
            {
                aimController.Initialize(InputHandler,playerController.PlayerColor);
                playerController.CanMove = false;
                playerController.CanSwitchPropUse = false;
                controllerInstance.aimController = aimController;
                
                if (entityHandler.TryGetComponet(EntityComponetUsage.PropContainer,out PlayerPropContainer container))
                {
                    container.ReplaceProp(this, controllerInstance);
                }
            }
        }
    }
}