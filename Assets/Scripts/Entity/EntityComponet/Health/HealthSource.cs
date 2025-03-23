using System;

/// <summary>
/// 血量资源，一个实体可以有多个血量资源，根据优先级不断扣除血量资源
/// </summary>
[Serializable]
public class HealthSource : IHitable
{
        public int BaseHealth;
        
        public float CurrentHealth;
        
        public int BaseMaxHealth;
        
        public float CurrentMaxHealth;
        
        public virtual void OnInitialized()
        {
                
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
                return CurrentHealth < 0;
        }

        public virtual void OnRemove()
        {
                
        }

        public virtual bool CanBeHit()
        {
                return true;
        }

        public virtual void Hit(DamageInfo damageInfo, out BeHittedInfo beHittedInfo)
        {
                if (!CanBeHit())
                {
                        beHittedInfo = BeHittedInfo.Default;
                        return;   
                }
                OnBeHitted(CurrentHealth,CurrentHealth - damageInfo.baseDamage,damageInfo);
                beHittedInfo = new BeHittedInfo()
                {
                        HasBeHittedDamage = Math.Min(damageInfo.baseDamage,CurrentHealth),
                        IsHitted = true
                };
                CurrentHealth -= damageInfo.baseDamage;
        }
}