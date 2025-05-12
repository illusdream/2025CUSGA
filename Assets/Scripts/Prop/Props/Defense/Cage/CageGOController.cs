using System;
using ilsFramework;

namespace Props
{
    public class CageGOController : EntityComponent
    {
        public const string Usage = "CageGOController";

        public override string TargetUsage => Usage;

        TimerCollection timers = new TimerCollection();
        
        public void Start()
        {
            if (BuffManager.Instance.TryGetBuffConfig(typeof(CageBuff),out CageBuffConfig buffConfig))
            {
                timers.CreateTimer(buffConfig.lastTime,1,"CageGO").SetOnFinish(OnFinish).Register();
            }

        }

        private void OnFinish(Timer timer)
        {
            Destroy(gameObject);
        }
        
        public void OnDestroy()
        {
            timers.ClearAllTimers();
        }
    }
}