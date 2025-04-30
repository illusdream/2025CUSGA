using System;
using System.Collections.Generic;
using DefaultNamespace;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Props
{
    public class LaserGun_LaserController : EntityComponent
    {
        public override string TargetUsage => "LaserGOController";

        public Collider2D attackCollider;

        public float LaserDamage;
        
        LaserGunPropConfig config;

        private List<EntityHandler> attackResult;

        public Transform Visual;
        
        bool HasAttacked = false;
        private float counter;

        private Vector3 baseSize;
        
        public CommenActionDirector Director;
        public void Start()
        {
            attackResult = new List<EntityHandler>();
            if (PropManager.Instance.TryGetPropConfig(typeof(LaserGunProp),out LaserGunPropConfig config))
            {
                this.config = config;
                LaserDamage = config.Damage;
            }

            if (handler.TryGetComponet(EntityComponetUsage.Attacker,out CommenAttacker attacker))
            {
                attacker.Damage = config.Damage;
            }
            baseSize = Visual.transform.localScale;
            Director.Play(config.LaserGameObjectTimeline);
            Director.onStopped+= DirectorOnonStopped;
        }

        private void DirectorOnonStopped(BaseActionDirector obj)
        {
            Destroy(this.gameObject);
        }


        [Button]
        public void TestAttack()
        {


        }

        public void Update()
        {
            return;
            counter += Time.deltaTime;
            Visual.localScale =new Vector3(baseSize.x,(1 - counter) *baseSize.y,baseSize.z);
            if (counter >0.5f && !HasAttacked)
            {
                if (handler.TryGetComponet(EntityComponetUsage.Attacker,out CommenAttacker commenAttacker))
                {
                    attackResult.Clear();
                    EntityManager.Instance.GetEntityInArea(attackCollider,config.AttackEntityType,attackResult);
                    foreach (var entityHandler in attackResult)
                    {
                        if (entityHandler.ID == handler.SpawnSource.SpawnerID)
                        {
                            continue;
                        }
                        DamageInfo damageInfo = DamageInfo.BuildDamageInfo(LaserDamage,ID);
                        commenAttacker.Attack(entityHandler);
                    }
                    //TestAttack();
                }
                HasAttacked = true;
            }

            if (counter>1)
            {
                
            }
        }
    }
}