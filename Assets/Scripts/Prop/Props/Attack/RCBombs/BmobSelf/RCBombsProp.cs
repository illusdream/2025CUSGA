using System;
using AreaInfos.Shapes;
using ilsFramework;
using UnityEngine;

namespace Props
{
    public class RCBombsProp : BaseProp,IPropSpawnEntity
    {
        public override Type ConfigType => typeof(RCBmobsPropConfig);
        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void SpawnEntity(PointShape pointShape, EntityHandler entityHandler, Transform pivotTransform)
        {
            var config = (RCBmobsPropConfig)this.config;
            pointShape.GetCurrentData(pivotTransform,out var point);
            var result =  Entity.Instantiate(config.RCBmobPrefab, entityHandler.SpawnEntityBySelf(),point,Quaternion.identity);

            var controllerInstance = PropManager.Instance.CreateTargetProp(typeof(RCBmobsControllerProp)) as RCBmobsControllerProp;
            
            
            if (result.TryGetComponent<RCBmobsGOController>(out var controller))
            {
                if (entityHandler.TryGetComponet(EntityComponetUsage.PropContainer,out PlayerPropContainer container))
                {
                    controller.Container = container;
                    container.ReplaceProp(this, controllerInstance);
                }

                controller.controllerInstance = controllerInstance;
                controllerInstance.GOController = controller;
            }
        }
    }
}