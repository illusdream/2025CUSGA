using System;
using System.Collections.Generic;
using AreaInfos.Shapes;
using DefaultNamespace;
using ilsFramework;
using Sirenix.OdinInspector;
using Unity.VisualScripting.FullSerializer;
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

        private float startDistance;
        
        private float oldDistance;
        
        private float oldDeltaDistance;

        public float slerpValue;

        public float deltaIncrease;

        public float maxDeltaValue;
        
        TimerCollection timerCollection;
        public void Start()
        {
            timerCollection = new TimerCollection();
            collidingResult = new List<EntityHandler>();

            if (PropManager.Instance.TryGetPropConfig(typeof(SwarmMissileProp),out  config) && handler.TryGetComponet(EntityComponetUsage.Attacker,out CommenAttacker attacker))
            {
                attacker.Damage = config.BaseDamage;
                timerCollection.CreateTimer(config.LifeTime, 1,"LifeTime").SetOnFinish(OnLifeEnd).Register();
            }
            
            if (TryGetTarget(out var target))
            {
                startDistance = Vector3.Distance(transform.position, target.transform.position);
            }
        }

        private void OnLifeEnd(Timer timer)
        {
            Destroy(gameObject);
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
            
            var cDistance = Vector3.Distance(transform.position, target.transform.position);
            
            var deltaDistance = cDistance - oldDistance;
            
            var d = deltaDistance - oldDeltaDistance;
            
            d = Mathf.Clamp(d, 0, maxDeltaValue);
            d /= maxDeltaValue;
            d = 1 - d;
            var dir = (target.transform.position - transform.position).normalized;

            
            
            if (handler.TryGetComponet(EntityComponetUsage.Moveable,out SwarmMissileMove moveable))
            {
                moveable.slerpValue = Mathf.Max((1 - cDistance / startDistance),0.3f) * slerpValue + d * deltaIncrease;
                moveable.Move(dir);
                var cv = moveable.GetEntityVelocity();
                var rot = Mathf.Atan2(cv.y, cv.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0,0,rot);
            }
            oldDistance = cDistance;
            oldDeltaDistance = deltaDistance;
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

        public void OnDestroy()
        {
            timerCollection.ClearAllTimers();
        }
    }
}