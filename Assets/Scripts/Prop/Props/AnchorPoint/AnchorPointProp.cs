using System;
using AreaInfos.Shapes;
using ilsFramework;
using UnityEngine;

namespace Props
{
    public class AnchorPointProp : BaseProp,IPropSpawnEntity
    {
        public override Type ConfigType => typeof(AnchorPointPropConfig);
        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void SpawnEntity(PointShape pointShape, EntityHandler entityHandler, Transform pivotTransform)
        {
            pointShape.GetCurrentData(pivotTransform,out var point);
            
            var config = (AnchorPointPropConfig)this.config;

            Entity.Instantiate(config.anchorPointPrefab, entityHandler.SpawnEntityBySelf(), point, Quaternion.identity);
        }
    }
}