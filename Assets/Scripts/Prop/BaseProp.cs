using System;

public abstract class BaseProp
{
        public abstract Type ConfigType { get;}

        public float BaseUsePropColdDown;

        public virtual void Initialize(BasePropConfig config)
        {
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
}