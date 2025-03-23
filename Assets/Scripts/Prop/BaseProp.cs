using System;

public abstract class BaseProp
{
        public abstract Type ConfigType { get;}
        public virtual void Initialize(BasePropConfig config) { }

        public virtual bool CanUseProp()
        {
                return true;
        }
        
        public abstract void UseProp();

        public virtual void Update()
        {
                
        }

        public virtual void FixedUpdate()
        {
                
        }

        public virtual void LateUpdate()
        {
                
        }

        public virtual void OnDestroy()
        {
                
        }
}