using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Props
{
    public class LaserGunProp : BaseProp
    {
        public override Type ConfigType => typeof(LaserGunPropConfig);

        LaserGunPropConfig config;
        
        Vector2 firDirection;
        public override void Initialize(BasePropConfig config)
        {
            if (config is LaserGunPropConfig LGPC)
            {
                this.config = LGPC;
            }
            base.Initialize(config);
        }

        public override void BeAddPropContainer(EntityHandler handler)
        {
            handler.AddEventListener(PlayerEvent.PlayerMoveCommend,EEntityEventScope.Component,Update_FireDir);
            base.BeAddPropContainer(handler);
        }

        public override void UseProp(EntityHandler handler)
        {
            var rot = Mathf.Atan2(firDirection.y, firDirection.x) * Mathf.Rad2Deg;
            Entity.Instantiate(config.projectilePrefab, handler.SpawnEntityBySelf(),handler.transform.position, Quaternion.Euler(0, 0, rot));
            //生成预制体
        }

        public override void BeRemovedFromContainer(EntityHandler handler)
        {
            base.BeRemovedFromContainer(handler);
        }

        public override void OnDestroy(EntityHandler handler)
        {
            base.OnDestroy(handler);
        }

        private void Update_FireDir(EventArgs args)
        {
            if (args is PlayerEvent.PlayerMoveCommendEventArgs cArgs)
            {
                firDirection = cArgs.PlayerMoveDirection;
            }
        }
    }
}