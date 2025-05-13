using System;
using DefaultNamespace;
using ilsFramework;
using UnityEngine;

public class MeteorGOController : EntityComponent
{
        public const string Usage = "meteorGO";

        public override string TargetUsage => Usage;


        public CommenAttacker commenAttacker;
        
        public CommenActionDirector commenActionDirector;

        public CommenTileHandler Handler;
        
        public Transform ShadowVisual;

        public Transform Meteor;
        
        TimerCollection timerCollection;

        private MeteorShowerConfig Config;
        
        private Vector3 startPosition;

        public float BombScale;
        public void Start()
        {
                startPosition = transform.position;
                timerCollection = new TimerCollection();
                if (RandomEventManager.Instance.TryGetRandomEventConfig(typeof(MeteorShower),out var _config) && _config is MeteorShowerConfig config)
                {
                        Config = config;
                        commenAttacker.Damage = config.MeteorDamage;
                        Handler.DamageToTile = config.MeteorDamage;
                        timerCollection.CreateTimer(config.MeteorAttackTime, 1, "Meteor").SetOnCycling(OnEnterCycling).SetOnFinish(OnFinish).Register();

                        AudioManager.Instance.Play(AudioChannelName.Sound, config.MeteorFallSound);
                }
        }

        private void OnEnterCycling(Timer timer)
        {
                ShadowVisual.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer.Progress);
                Meteor.position = Vector3.Lerp(startPosition, new Vector3(startPosition.x,startPosition.y,-0.1f), timer.Progress);
        }

        private void OnFinish(Timer timer)
        {
                commenActionDirector.TryPlay(Config.MeteorAttackTimeline);
                commenActionDirector.onStopped += CommenActionDirectorOnonStopped;
        }

        private void CommenActionDirectorOnonStopped(BaseActionDirector obj)
        {
                if (VisualEffectManager.Instance.TryGetVisualEffectPool(out ExplosionVE ve))
                {
                        ve.TryEmittingVE(transform.position,Vector2.one *BombScale); 
                }
                Destroy(transform.parent.gameObject);
        }


        public void OnDestroy()
        {
                timerCollection.ClearAllTimers();
        }
}