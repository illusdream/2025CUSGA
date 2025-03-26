using System;
using ilsFramework;

public class PlayerEnergyContainer : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.EnergyContainer;
        
        public int MaxEnergy;
        
        public float CurrentEnergy;

        public float EnergyCanBeComeProp =100;
        
        public float GetCurrentEnergy()
        {
                return CurrentEnergy;
        }

        public override void OnInitialized(EntityHandler handler)
        {
                TileManager.Instance.AddListener(TileEvent.TileMerged,Listener_TileMerge);
                base.OnInitialized(handler);
        }

        public override void OnEntityDestroy(EntityHandler handler)
        {
                TileManager.Instance.RemoveListener(TileEvent.TileMerged,Listener_TileMerge);
                base.OnEntityDestroy(handler);
        }

        private void Listener_TileMerge(EventArgs args)
        {
                if (args is TileEvent.TileMergeEventArgs eventArgs)
                {
                        if (eventArgs.scoreCollection.TryGetValue(ID,out var value))
                        {
                                AddEnergy(value);
                        }
                }
        }

        public void CumsumEnergy(float energy)
        {
                CurrentEnergy -= energy;
        }

        public void AddEnergy(float energy)
        {
                CurrentEnergy += energy;
                
                if (CurrentEnergy > EnergyCanBeComeProp)
                {
                        BroadcastEvent(PlayerEvent.HasEnoughEnergyToMakeProp,EEntityEventScope.Component,new PlayerEvent.HasEnoughEnergyToMakePropEventArgs(this));
                }
                
        }
        
        public void Update()
        {
                
        }

        public void FixedUpdate()
        {

        }
}