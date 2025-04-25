using System;
using AreaInfos.Shapes;
using ilsFramework;
using UnityEngine;

namespace Props
{
    public class ReflectingPrismProp : BaseProp,IPropSpawnEntity
    {
        public override Type ConfigType => typeof(ReflectingPrismPropConfig);
        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void SpawnEntity(PointShape pointShape, EntityHandler entityHandler, Transform pivotTransform)
        {
            var config = (ReflectingPrismPropConfig)this.config;
            pointShape.GetCurrentData(pivotTransform,out var point);
            var result =  Entity.Instantiate(config.ReflectingPrismPrefab, entityHandler.SpawnEntityBySelf(),point,pivotTransform.rotation);
        }
    }
}