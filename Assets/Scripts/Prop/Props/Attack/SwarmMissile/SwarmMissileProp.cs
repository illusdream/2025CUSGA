using System;
using AreaInfos.Shapes;
using ilsFramework;
using UnityEngine;
using Utils;

namespace Props
{
    public class SwarmMissileProp : BaseProp,IPropSpawnEntity,IPropStartAnimation
    {
        private Vector2 dir;
        public override Type ConfigType =>typeof(SwarmMissilePropConfig);
        public override void UseProp(EntityHandler handler)
        {
            
        }

        public void SpawnEntity(PointShape pointShape, EntityHandler entityHandler, Transform pivotTransform)
        {
            var config = (SwarmMissilePropConfig)this.config;
            pointShape.GetCurrentData(pivotTransform,out var point);
            var result =  Entity.Instantiate(config.MissilePrefab, entityHandler.SpawnEntityBySelf(),point,Quaternion.identity);
            if (result.TryGetComponent<BaseEntityMove>(out var moveable))
            {
                var vel = (point.Vec3_xy() - pivotTransform.position).normalized * config.SpawnVelocity;
                moveable.SetTargetVelocity(vel);
            }
        }

        public void OnStartAnimation()
        {
            dir = InputHandler.LastActiveMoveDirection;
        }
    }
}