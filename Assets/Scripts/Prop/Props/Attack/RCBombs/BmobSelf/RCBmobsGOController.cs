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
        
        public void Start()
        {
            timerCollection = new TimerCollection();
            PropManager.Instance.TryGetPropConfig(typeof(RCBombsProp),out config);

            commenAttacker.Damage = config.DamageToEntity;
            commenTileHandler.DamageToTile = config.DamageToTile;
            
            timerCollection.CreateTimer(config.TimeToBmob,1,Timer_ToBmob).SetOnFinish(StartBomb).Register();
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
        }

        private void CommenActionDirectorOnonStopped(BaseActionDirector obj)
        {
            Destroy(gameObject);
            Container.RemoveProp(controllerInstance);
        }
    }
}