using System;
using AreaInfos.Shapes;
using UnityEngine;

namespace Props
{
    public class AcceleratingFieldProp : BaseProp,IPropSpawnEntity
    {
        public override Type ConfigType => typeof(AcceleratingFieldPropConfig);
        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void SpawnEntity(PointShape pointShape, EntityHandler entityHandler, Transform pivotTransform)
        {
            pointShape.GetCurrentData(pivotTransform,out var point);
            
            var config = (AcceleratingFieldPropConfig)this.config;

            Entity.Instantiate(config.AcceleratingFieldPrefab, entityHandler.SpawnEntityBySelf(), point, Quaternion.identity);
        }
    }
}