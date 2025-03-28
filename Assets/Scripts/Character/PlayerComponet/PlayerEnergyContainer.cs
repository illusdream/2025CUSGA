using System;
using ilsFramework;
using Sirenix.OdinInspector;

public class PlayerEnergyContainer : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.EnergyContainer;
        
        public int MaxEnergy { get;private set; }
        [ShowInInspector]
        public float CurrentEnergy { get;private set; }
        
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
        }
        
        public void Update()
        {
                
        }

        public void FixedUpdate()
        {

        }
}