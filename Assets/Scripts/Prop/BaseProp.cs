using System;
using UnityEngine;
using UnityEngine.Timeline;

public abstract class BaseProp
{
        public abstract Type ConfigType { get;}

        public float BaseUsePropColdDown;

        protected BasePropConfig config;

        public PlayerInputHandler InputHandler;
        
        public virtual Type PropStateType => typeof(DefaultPropState);
        
        public int PropUseCount { get; set; }

        public virtual void Initialize(BasePropConfig config)
        {
                this.config = config;
                PropUseCount = config.PropCanUseCount;
                BaseUsePropColdDown = config.BasePropUseColdDown;
        }

        public virtual void BeAddPropContainer(EntityHandler handler){}
        
        public virtual bool CanUseProp(EntityHandler handler)
        {
                return true;
        }
        
        public abstract void UseProp(EntityHandler handler);

        public virtual void Update(EntityHandler handler)
        {
                
        }

        public virtual void FixedUpdate(EntityHandler handler)
        {
                
        }

        public virtual void LateUpdate(EntityHandler handler)
        {
                
        }

        public virtual void BeRemovedFromContainer(EntityHandler handler)
        {
                
        }
        
        public virtual void OnDestroy(EntityHandler handler)
        {
                
        }

        public virtual float GetUsePropColdDown(EntityHandler handler)
        {
                return BaseUsePropColdDown;
        }

        public virtual TimelineAsset GetPlayTimelineAsset(PlayerController playerController)
        {
                return config.PlayAsset;
        }

        public void SetInputHandler(PlayerInputHandler inputHandler)
        {
                InputHandler = inputHandler;
        }

        public virtual bool CanConsume(EntityHandler handler,PlayerController playerController)
        {
                return true;
        }
}