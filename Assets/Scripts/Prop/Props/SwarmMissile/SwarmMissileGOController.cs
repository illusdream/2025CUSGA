using System;
using System.Collections.Generic;
using AreaInfos.Shapes;
using DefaultNamespace;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Props
{
    public class SwarmMissileGOController : EntityComponent
    {
        public const string targetUsage = "SwarmMissileGOController";
        public override string TargetUsage => targetUsage;

        public BoxShape ColliderShape;
        
        bool hasCollison = false;

        private List<EntityHandler> collidingResult;

        public float LifeTime;
        
        public SwarmMissilePropConfig config;
        
        public CommenActionDirector Director;
        public void Start()
        {
            collidingResult = new List<EntityHandler>();

            if (PropManager.Instance.TryGetPropConfig(typeof(SwarmMissileProp),out  config) && handler.TryGetComponet(EntityComponetUsage.Attacker,out CommenAttacker attacker))
            {
                attacker.Damage = config.BaseDamage;
            }
        }

        public void Update()
        { 
            
        }


        public void FixedUpdate()
        {
            if (!TryGetTarget(out var target))
            {
                return;
            }

            if (!hasCollison && CheckIsCollidingWithTarget(target))
            {
                hasCollison = true;
                Director.Play(config.AttackTimeline);
                Director.onStopped += DirectorOnonStopped;
                //
                
                //放动画，并造成伤害
            }
            
            var dir = (target.transform.position - transform.position).normalized;
            if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove moveable))
            {
                moveable.Move(dir);
                var cv = moveable.GetEntityVelocity();
                var rot = Mathf.Atan2(cv.y, cv.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0,0,rot);
            }
            

        }

        private void DirectorOnonStopped(BaseActionDirector obj)
        {
            Destroy(this.gameObject);
        }

        public bool CheckIsCollidingWithTarget(PlayerController target)
        {
            collidingResult.Clear();
            EntityManager.Instance.GetEntityOverlapByShape(transform, ColliderShape, EEntityType.Character, collidingResult);
            
            return collidingResult.Contains(target.handler);
        }
        
        
        public bool TryGetTarget(out PlayerController target)
        {
            //是第一个发射的
            if (handler.SpawnSource.SpawnerID == CharacterManager.Instance.Player1Controller.ID)
            {
                target = CharacterManager.Instance.Player2Controller;
                return true;
            }
            //是第二个发射的
            if (handler.SpawnSource.SpawnerID == CharacterManager.Instance.Player2Controller.ID)
            {
                target = CharacterManager.Instance.Player1Controller;
                return true;
            }
            target = null;
            return false;
        }

        public void StartLifeTimer(float lifeTime)
        {
            
        }

        public void Test(HashSet<EntityHandler> hashSet)
        {
            
        }
    }
}