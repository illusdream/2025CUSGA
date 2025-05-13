using System;
using DefaultNamespace;
using ilsFramework;
using UnityEngine;

namespace Props
{
    public class RCBmobsGOController : EntityComponent
    {
        public const string Usage = "RCBmobsGOController";

        public override string TargetUsage => Usage;

        public CommenActionDirector commenActionDirector;
        
        public CommenAttacker commenAttacker;
        
        public CommenTileHandler commenTileHandler;

        public RCBmobsControllerProp controllerInstance;

        public PlayerPropContainer Container;

        private RCBmobsPropConfig config;
        
        TimerCollection timerCollection;

        private const string Timer_ToBmob = "TimerToBmob";


        public SoundData didiSound;

        public float BombScale;
        
        public AudioEmitter emitter;
        public void Start()
        {
            timerCollection = new TimerCollection();
            PropManager.Instance.TryGetPropConfig(typeof(RCBombsProp),out config);

            commenAttacker.Damage = config.DamageToEntity;
            commenTileHandler.DamageToTile = config.DamageToTile;
            
            timerCollection.CreateTimer(config.TimeToBmob,1,Timer_ToBmob).SetOnFinish(StartBomb).Register();
            
            emitter= AudioManager.Instance.Play(AudioChannelName.Sound,didiSound);
        }


        public void ImmediateToBomb()
        {
            StartBomb(null);
        }

        private void StartBomb(Timer timer)
        {
            commenActionDirector.Play(config.BmobAsset);
            commenActionDirector.onStopped += CommenActionDirectorOnonStopped;
            timerCollection.RemoveTimer(Timer_ToBmob);
            emitter.Stop();
        }

        private void CommenActionDirectorOnonStopped(BaseActionDirector obj)
        {
            if (VisualEffectManager.Instance.TryGetVisualEffectPool(out ExplosionVE ve))
            {
                ve.TryEmittingVE(transform.position,Vector2.one *BombScale); 
            }
            Destroy(gameObject);
            Container.RemoveProp(controllerInstance);
        }
    }
}