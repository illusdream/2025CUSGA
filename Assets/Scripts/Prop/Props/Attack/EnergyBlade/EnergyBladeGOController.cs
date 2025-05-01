using System;
using DefaultNamespace;
using ilsFramework;
using UnityEngine;

namespace Props
{
    public class EnergyBladeGOController : EntityComponent
    {
        public  const string Usage = "EnergyBladeGOController";

        public override string TargetUsage => Usage;

        private Transform targetPlayer;

        private EnergyBladePropConfig Config;

        public CommenAttacker Attacker;

        public CommenActionDirector Director;
        public void Start()
        {
            if (CharacterManager.Instance.TryGetPlayerController(handler.SpawnSource.SpawnerID, out PlayerController player))
            {
                targetPlayer = player.transform;
            }
            if (PropManager.Instance.TryGetPropConfig(typeof(EnergyBladeProp),out Config))
            {
                Attacker.Damage = Config.BladeSingleDamage;
                Director.Play(Config.EnergyBladeTimelineAsset);
                Director.onStopped += DirectorOnonStopped;
            }
        }

        private void DirectorOnonStopped(BaseActionDirector obj)
        {
            Destroy(gameObject);
        }


        public void Update()
        {
            transform.position = targetPlayer.position;
        }
    }
}