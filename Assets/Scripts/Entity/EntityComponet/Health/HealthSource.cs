using System;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// 血量资源，一个实体可以有多个血量资源，根据优先级不断扣除血量资源
/// </summary>
[Serializable]
public class HealthSource : IHitable
{
        public int BaseHealth;
        [ShowInInspector] 
        public float CurrentHealth { get;private set; }
        
        public int BaseMaxHealth;
        
        public float CurrentMaxHealth{ get;private set; }

        public event Action<float, float, DamageInfo> OnBeHittedEvent;
        
        public virtual void OnInitialized()
        {
                CurrentHealth = BaseHealth;
                CurrentMaxHealth = BaseMaxHealth;
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
                
        }

        public virtual void LateUpdate()
        {
                
        }

        public virtual void OnBeHitted(float beforeHitHealth,float afterHitHealth,DamageInfo damageInfo)
        {
                
        }
        
        /// <summary>
        /// 是否耗尽血量资源
        /// </summary>
        /// <returns></returns>
        public virtual bool IsDepleted()
        {
                return CurrentHealth <= 0;
        }

        public virtual void OnRemove()
        {
                
        }

        public virtual bool CanBeHit()
        {
                return !IsDepleted();
        }

        public virtual void Hit(DamageInfo damageInfo, out BeHittedInfo beHittedInfo)
        {
                if (!CanBeHit())
                {
                        beHittedInfo = BeHittedInfo.Default;
                        return;   
                }

                var finalHealth = CurrentHealth - damageInfo.baseDamage;
                OnBeHittedEvent?.Invoke(CurrentHealth,finalHealth,damageInfo);
                OnBeHitted(CurrentHealth,finalHealth,damageInfo);
                beHittedInfo = new BeHittedInfo()
                {
                        HasBeHittedDamage = Math.Min(damageInfo.GetFinalApplyDamage(),CurrentHealth),
                        IsHitted = true
                };
                CurrentHealth = finalHealth;
        }

        public virtual void AddValue(float value)
        {
                CurrentHealth += value;
        }

        public virtual void SetMaxHealth(float value)
        {
                CurrentMaxHealth = value;
        }

        public virtual void SetCurrentHealth(float value)
        {
                CurrentHealth = value;
        }
}