using System;
using System.Collections.Generic;
using DefaultNamespace;
using ilsFramework;
using UnityEngine;

namespace Props
{
    public class HandGrenadeGOController : EntityComponent
    {
        public const string Usage = "HandGrenadeGOController";

        public override string TargetUsage => Usage;

        public CommenAttacker Attacker;
        
        public CommenTileHandler TileHandler;
        
        public CommenActionDirector ActionDirector;

        public HandGrenadePropConfig Config;

        public float BombScale;
        
        public override void OnInitialized(EntityHandler handler)
        {

            base.OnInitialized(handler);
        }


        public void Start()
        {
            if (PropManager.Instance.TryGetPropConfig(typeof(HandGrenadeProp),out Config))
            {
                Attacker.Damage = Config.BaseDamage;
                TileHandler.DamageToTile = Config.BaseDamage / Time.fixedDeltaTime;
                ActionDirector.Play(Config.HandGrenadeBoomTimeline);
                ActionDirector.onStopped += ActionDirectorOnonStopped;
            }
            
        }

        private void ActionDirectorOnonStopped(BaseActionDirector obj)
        {
            if (VisualEffectManager.Instance.TryGetVisualEffectPool(out ExplosionVE ve))
            {
                ve.TryEmittingVE(transform.position,Vector2.one *BombScale); 
            }
            Destroy(gameObject);
        }


    }
}