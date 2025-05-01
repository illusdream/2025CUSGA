using System;
using AreaInfos.Shapes;
using DefaultNamespace;
using ilsFramework;
using UnityEngine;
using Utils;

namespace Props
{
    public class MortarMissileGOController : EntityComponent
    {
        public const string Usage = "MortarMissileGOController";

        public override string TargetUsage => Usage;
        
        public BaseEntityMove move;

        public Vector3 targetPosition;

        public float MaxSpeed;

        public float AccTime;
        
        public CommenActionDirector Director;
        
        public CommenAttacker Attacker;

        public bool IsUp  =true;

        public float MaxFallDownDistance;

        public Vector3 oldPosition;

        public float HasWalkDistance;
        
        public float CurrentRotation => transform.rotation.eulerAngles.z;

        public TimerCollection TimerCollection;

        private MortarPropConfig propConfig;
        public void Start()
        {
            TimerCollection = new TimerCollection();
            transform.rotation = Quaternion.Euler(0,0,90);
            
            move.MaxSpeed = MaxSpeed;

            if (PropManager.Instance.TryGetPropConfig(typeof(MortarProp),out  propConfig))
            {
                Attacker.Damage = propConfig.Damage;
            }
            
            TimerCollection.CreateTimer(AccTime,1,"MortarMissileGO").SetOnCycling(OnUpTimerCycling).SetOnFinish(OnUpTimerFinish).Register();
        }

        public void Update()
        {

        }
        bool hasTriggered = false;
        public void FixedUpdate()
        {
            if (!IsUp)
            {
                HasWalkDistance += Vector3.Distance(oldPosition,transform.position);
                if (HasWalkDistance > MaxFallDownDistance && !hasTriggered)
                {
                    Director.Play(propConfig.MortarExplosionTimelineAsset);
                    Director.onStopped += DirectorOnonStopped;
                    hasTriggered = true;
                }
                oldPosition = transform.position;
            }
        }

        private void DirectorOnonStopped(BaseActionDirector obj)
        {
            Destroy(gameObject);
        }

        public void OnUpTimerCycling(Timer timer)
        {
            var cSpeed = Mathf.Lerp(0, MaxSpeed, timer.Progress);
            move.SetTargetVelocity(cSpeed * Vector2.left.Rotate(-CurrentRotation * Mathf.Deg2Rad));
        }

        public void OnUpTimerFinish(Timer timer)
        {
            if (!Mathf.Approximately(CurrentRotation, 90))
            {
                Destroy(gameObject);
            }
            //跳转
            move.transform.rotation = Quaternion.Euler(0,0,-90);
            move.SetTargetVelocity(MaxSpeed * Vector2.left.Rotate(-CurrentRotation * Mathf.Deg2Rad));
            move.transform.position = targetPosition + Vector3.up * MaxFallDownDistance;
            oldPosition = transform.position;
            IsUp = false;
        }


        public void OnDestroy()
        {
            TimerCollection.ClearAllTimers();
        }
    }
}