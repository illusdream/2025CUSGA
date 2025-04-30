using System;
using ilsFramework;

namespace Props
{
    public class YellowDuckProp : BaseProp
    {
        public override Type PropStateType => typeof(YellowDuckPropState);

        public bool HasBeenUsed = false;
        private TimerCollection timerCollection = new TimerCollection();
        
        private PlayerController playerController;
        
        private EntityHandler handler;
        
        public override Type ConfigType => typeof(YellowDuckPropConfig);
        public override void UseProp(EntityHandler handler)
        {
            this.handler = handler;
            this.handler.TryGetComponet(EntityComponetUsage.playerController, out playerController);
            if (!HasBeenUsed)
            {
                timerCollection.CreateTimer(((YellowDuckPropConfig)config).CanUseTime, 1, "YellowDuckCanUse").SetOnFinish(OnFinished).Register();
                HasBeenUsed = true;
            }

            if (handler.TryGetComponet(EntityComponetUsage.EnergyContainer, out PlayerEnergyContainer container))
            {
                container.AddEnergy(((YellowDuckPropConfig)config).PerPressApplyEnergy);
            }
        }

        public override bool CanConsume(EntityHandler handler, PlayerController playerController)
        {
            return false;
        }

        private void OnFinished(Timer timer)
        {
            playerController.CanSwitchPropUse = true;
            if (handler.TryGetComponet(EntityComponetUsage.PropContainer,out PlayerPropContainer container))
            {
                container.RemoveProp(this);
            }
        }
    }
}