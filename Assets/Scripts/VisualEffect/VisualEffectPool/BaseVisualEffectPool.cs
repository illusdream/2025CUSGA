using System;
using ilsFramework;
using UnityEngine;

public abstract class BaseVisualEffectPool
{
        public GameObjectPool pool;
        
        public abstract Type ConfigType { get; }
        
        public BaseVisualEffectConfig _config;

        public GameObject VisualPoolContainer;
        
        public abstract void InitPool();

        public abstract bool TryGetPool(out GameObject poolObject);

        public abstract void ReleasePool(GameObject poolObject);
        
        public abstract void PoolOnDestroy();

        public abstract void Clear();

}

public abstract class BaseVisualEffectPool<T> : BaseVisualEffectPool where T : BaseVisualEffectConfig, new()
{
        public T Config => (T)this._config;

        public override Type ConfigType => typeof(T);
}