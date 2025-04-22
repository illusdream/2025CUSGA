using System;
using AreaInfos.Shapes;
using ilsFramework;
using Unity.Mathematics;
using UnityEngine;

namespace Props
{
    public class HandGrenadeProp :  BaseProp,IPropSpawnEntity
    {
        public override Type ConfigType => typeof(HandGrenadePropConfig);
        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void SpawnEntity(PointShape pointShape, EntityHandler entityHandler, Transform pivotTransform)
        {
            pointShape.GetCurrentData(pivotTransform,out var point);
            
            var config = (HandGrenadePropConfig)this.config;

            Entity.Instantiate(config.HandGrenadeBoomerPrefab, entityHandler.SpawnEntityBySelf(), point, quaternion.identity);

        }
    }
}