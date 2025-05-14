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
        private static readonly int Loop = Shader.PropertyToID("_MainStep");
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

        public SpriteRenderer Main;
        public MaterialPropertyBlock MainPropertyBlock;
        public SpriteRenderer Outline1;
        public MaterialPropertyBlock Outline1PropertyBlock;
        public SpriteRenderer Outline2;
        public MaterialPropertyBlock Outline2PropertyBlock;
        
        TimerCollection TimerCollection = new TimerCollection();

        public float pros;
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
            
            MainPropertyBlock = new MaterialPropertyBlock();
            Outline1PropertyBlock = new MaterialPropertyBlock();
            Outline2PropertyBlock = new MaterialPropertyBlock();
            
            Main.GetPropertyBlock(MainPropertyBlock);
            Outline1.GetPropertyBlock(Outline1PropertyBlock);
            Outline2.GetPropertyBlock(Outline2PropertyBlock);

            TimerCollection.CreateTimer(1, 1, "LaserShow").SetOnCycling(OnLaserShow).Register();
        }

        private void OnLaserShow(Timer timer)
        {
            pros = timer.Progress;
            MainPropertyBlock.SetFloat(Loop,timer.Progress);
            Outline1PropertyBlock.SetFloat(Loop,timer.Progress);
            Outline2PropertyBlock.SetFloat(Loop,timer.Progress);
            Main.SetPropertyBlock(MainPropertyBlock);
            Outline1.SetPropertyBlock(Outline1PropertyBlock);
            Outline2.SetPropertyBlock(Outline2PropertyBlock);
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

        public void LateUpdate()
        {
            
        }

        public void OnDestroy()
        {
            TimerCollection.ClearAllTimers();
        }
    }
}