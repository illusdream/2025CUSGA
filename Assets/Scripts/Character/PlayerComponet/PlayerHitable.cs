using System;

public class PlayerHitable : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.Hitable;

        public override void OnInitialized(EntityHandler handler)
        {
                AddEventListener(EntityEvent.EntityBeHitted,EEntityEventScope.Entity,Listener_EntityBeHitted);
                base.OnInitialized(handler);
        }

        public override void OnEntityDestroy(EntityHandler handler)
        {
                RemoveEventListener(EntityEvent.EntityBeHitted,EEntityEventScope.Entity,Listener_EntityBeHitted);
                base.OnEntityDestroy(handler);
        }

        private void Listener_EntityBeHitted(EventArgs _args)
        {
                
                //进行内部处理，然后广播内部事件
                //暂时就重新计算下数值，然后扔到Health中处理吧
                if (_args is EntityEvent.EntityBeHittedEventArgs args)
                {
                        DamageInfo inner = DamageInfo.BuildDamageInfo(args.damageInfo.GetFinalApplyDamage(),args.damageInfo.DamageFrom);
                        
                        BroadcastEvent(PlayerEvent.BeHitted,EEntityEventScope.Component,new PlayerEvent.BeHittedEventArgs(inner));
                }
        }
}