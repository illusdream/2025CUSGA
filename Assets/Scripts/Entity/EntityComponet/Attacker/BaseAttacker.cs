using System;


public abstract class BaseAttacker : EntityComponent
{
        public override string TargetUsage=> EntityComponetUsage.Attacker;
        
        
        //就写个攻击函数吧，其他爱鸡巴干嘛干嘛
        public event Action<EntityHandler,DamageInfo,BeHittedInfo> OnAttack;

        public void InvokeOnAttack(EntityHandler target,DamageInfo damageInfo,BeHittedInfo beHittedInfo)
        {
                OnAttack?.Invoke(target,damageInfo,beHittedInfo);
        }
        
        public abstract void Attack(EntityHandler target);
}