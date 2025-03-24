using System;
using System.Collections.Generic;
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
        public void Start()
        {
            attackResult = new List<EntityHandler>();
            if (PropManager.Instance.TryGetPropConfig(typeof(LaserGunProp),out LaserGunPropConfig config))
            {
                this.config = config;
                LaserDamage = config.Damage;
            }
            baseSize = Visual.transform.localScale;
        }


        [Button]
        public void TestAttack()
        {
            attackResult.Clear();
            EntityManager.Instance.GetEntityInArea(attackCollider,config.AttackEntityType,attackResult);
            foreach (var entityHandler in attackResult)
            {
                DamageInfo damageInfo = DamageInfo.BuildDamageInfo(LaserDamage,ID);
                entityHandler.BroadcastEvent(EntityEvent.EntityBeHitted,EEntityEventScope.Entity,new EntityEvent.EntityBeHittedEventArgs(damageInfo));
                entityHandler.ID.LogSelf();
            }
        }

        public void Update()
        {
            counter += Time.deltaTime;
            Visual.localScale =new Vector3(baseSize.x,(1 - counter) *baseSize.y,baseSize.z);
            if (counter >0.5f && !HasAttacked)
            {
                TestAttack();
                HasAttacked = true;
            }

            if (counter>1)
            {
                Destroy(this.gameObject);
            }
        }
    }
}