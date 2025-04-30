using System;
using AreaInfos.Shapes;
using ilsFramework;
using UnityEngine;

namespace Props
{
    public class BlackHoleCannonProp : BaseProp,IPropSpawnEntity
    {
        public override Type ConfigType => typeof(BlackHoleCannonPropConfig);
        public override void UseProp(EntityHandler handler)
        {
           
        }

        public void SpawnEntity(PointShape pointShape, EntityHandler entityHandler, Transform pivotTransform)
        {
            var config = (BlackHoleCannonPropConfig)this.config;
            pointShape.GetCurrentData(pivotTransform,out var point);
            var result =  Entity.Instantiate(config.blackHolePrefab, entityHandler.SpawnEntityBySelf(),point,Quaternion.identity);
            if (result.TryGetComponent<BaseEntityMove>(out var moveable))
            {
                var vel = (point.Vec3_xy() - pivotTransform.position).normalized * config.blackHoleSpeed;
                moveable.SetTargetVelocity(vel);
            }
        }
    }
}