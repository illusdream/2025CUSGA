using System;
using AreaInfos.Shapes;
using UnityEngine;

namespace Props
{
    public class MortarControllerProp : BaseProp,IPropSpawnEntity
    {
        public override Type ConfigType => typeof(MortarControllerPropConfig);
        
        public MortarAimController aimController;
        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void SpawnEntity(PointShape pointShape, EntityHandler entityHandler, Transform pivotTransform)
        {
            if (entityHandler.TryGetComponet(EntityComponetUsage.playerController, out PlayerController playerController))
            {
                playerController.SetCanMove(true);
                playerController.CanSwitchPropUse = true;
                playerController.CanUpdatePlayerDirection = true;
            }

            //生成Aim
            var config = (MortarControllerPropConfig)this.config;
            pointShape.GetCurrentData(pivotTransform,out var point);
            var result =  Entity.Instantiate(config.mortarPrefab, entityHandler.SpawnEntityBySelf(),point,Quaternion.identity);

            if (result.TryGetComponent<MortarMissileGOController>(out var goController))
            {
                goController.targetPosition = aimController.transform.position;
            }
            
            
            aimController.EndAim();
        }
    }
}