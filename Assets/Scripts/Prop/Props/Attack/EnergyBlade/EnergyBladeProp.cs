using System;
using AreaInfos.Shapes;
using Unity.Mathematics;
using UnityEngine;

namespace Props
{
    public class EnergyBladeProp : BaseProp,IPropSpawnEntity
    {
        public override Type ConfigType => typeof(EnergyBladePropConfig);
        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void SpawnEntity(PointShape pointShape, EntityHandler entityHandler, Transform pivotTransform)
        {
            pointShape.GetCurrentData(pivotTransform,out var point);
            
            var config = (EnergyBladePropConfig)this.config;

            Entity.Instantiate(config.EnergyBladePrefab, entityHandler.SpawnEntityBySelf(), point, pivotTransform.rotation);
        }
    }
}