using System;
using AreaInfos.Shapes;
using DefaultNamespace;
using ilsFramework;
using UnityEditor;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace Props
{
    public class LaserGunProp : BaseProp,IPropVisualControl,IPropSpawnEntity,IPropUpdate
    {
        public override Type ConfigType => typeof(LaserGunPropConfig);
        
        LaserGunPropConfig config;
        private EntityHandler handler;
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
            this.handler = handler;
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


        private float TargetAngle;
        private float startAngle;
        private Vector2 dir;
        private float currectRot;
        public void OnStartVisualModifier(Transform visualTransform)
        {
            dir = InputHandler.LastActiveMoveDirection;
            TargetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            startAngle = visualTransform.eulerAngles.z;
            
            currectRot = TargetAngle;
            if (TargetAngle < 0)
            {
                TargetAngle += 360;
            }

        }

        public void ProcessVisualModifier(Transform visualTransform, double clipDuration, double clipCurrentTime)
        {
            var c = (float)(clipCurrentTime / clipDuration);
            var value = Mathf.LerpAngle(startAngle, TargetAngle, config.lerpCurve.Evaluate(c));

            if (handler.TryGetComponet(EntityComponetUsage.playerVisualHandler,out PlayerVisualController playerVisualController))
            {
                playerVisualController.SetRotation(value);
            }
        }
        
        public void OnEndVisualModifier(Transform visualTransform)
        {
            visualTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        public void SpawnEntity(PointShape pointShape, EntityHandler entityHandler, Transform pivotTransform)
        {

            var rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            pointShape.GetCurrentData(pivotTransform,out var point);
            Entity.Instantiate(config.projectilePrefab, entityHandler.SpawnEntityBySelf(),point, Quaternion.Euler(0, 0, rot));
            if (entityHandler.TryGetComponet(EntityComponetUsage.Moveable,out PlayerMoveComponent component))
            {
                component.AddForce(-dir.normalized*config.ShootForce, ForceMode2D.Impulse);
            }

        }

        public void UpdateOnUsingProp(EntityHandler handler)
        {
           
            if (handler.TryGetComponet(EntityComponetUsage.Moveable, out PlayerMoveComponent component) && handler.TryGetComponet(EntityComponetUsage.playerController,out PlayerController controller))
            {
                var dir = controller.playerInputHandler.Move.ActionValue;
                component.Move(dir);
            }
        }
    }
}