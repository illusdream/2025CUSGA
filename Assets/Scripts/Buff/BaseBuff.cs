using System;
using ilsFramework;

public abstract class BaseBuff
{
        
        public abstract Type ConfigType { get; }
        /// <summary>
        /// buffer内部自带的计时器
        /// </summary>
        protected Timer buffTimer;

        public bool IsExist => !(buffTimer?.IsFinish).GetValueOrDefault(false);

        protected BaseBuffConfig _config;

        public EBuffType BuffName;
        
        public virtual EBuffTag BuffTag => EBuffTag.None;
        
        public virtual void AddBuff(EntityHandler handler)
        {
                if (BuffManager.Instance.TryGetBuffConfig(GetType(),out BaseBuffConfig config))
                {
                        _config = config;
                }
                
                buffTimer = (new TimerBuilder(config.lastTime, 1)).Register();
                
                OnAddBuff(handler);
        }
        
        protected abstract void OnAddBuff(EntityHandler handler);

        public virtual void BuffTick(EntityHandler handler)
        {
                OnBuffTick(handler);
        }
        
        protected abstract void OnBuffTick(EntityHandler handler);

        public virtual void RemoveBuff(EntityHandler handler)
        {
                OnRemoveBuff(handler);
        }
        
        protected abstract void OnRemoveBuff(EntityHandler handler);

        public void ResetBuffTimer()
        {
                buffTimer.Reset(_config.lastTime,0,1,ETimerType.TimeScale,null,null,null,null);
                OnResetBuffTimer();
        }
        
        public abstract void OnResetBuffTimer();
        
}


public abstract class BaseBuff<T> : BaseBuff where T : BaseBuffConfig
{
        public T Config => (T)this._config;

        public override Type ConfigType => typeof(T);
}