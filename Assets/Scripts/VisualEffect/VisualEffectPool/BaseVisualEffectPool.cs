using System;
using ilsFramework;
using UnityEngine;

public abstract class BaseVisualEffectPool
{
        GameObjectPool pool;
        
        public abstract Type ConfigType { get; }
        
        protected BaseVisualEffectConfig _config;

        public abstract void InitPool();

        public abstract bool TryGetPool(out GameObject poolObject);

        public abstract void ReleasePool(GameObject poolObject);
        
        public abstract void PoolOnDestroy();
        
}

public abstract class BaseVisualEffectPool<T> : BaseVisualEffectPool where T : BaseVisualEffectConfig, new()
{
        public T Config => (T)this._config;

        public override Type ConfigType => typeof(T);
}