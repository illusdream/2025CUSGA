using System;
using DefaultNamespace;
using ilsFramework;

public class PlayerHitable : BaseHitable
{
        public override string TargetUsage => EntityComponetUsage.Hitable;

        public override void OnInitialized(EntityHandler handler)
        {
               
                base.OnInitialized(handler);
        }

        public override void OnEntityDestroy(EntityHandler handler)
        {
              
                base.OnEntityDestroy(handler);
        }

        private void Listener_EntityBeHitted(EventArgs _args)
        {
                
                //进行内部处理，然后广播内部事件
                //暂时就重新计算下数值，然后扔到Health中处理吧
                if (_args is EntityEvent.EntityBeHittedEventArgs args)
                {
                     
                }
        }

        public override bool CanBeHit()
        {
                if (handler.TryGetComponet(EntityComponetUsage.Health,out PlayerHealth health))
                {
                      return  health.CanBeHit();
                }
                return base.CanBeHit();
        }

        public override void Hit(DamageInfo damageInfo, out BeHittedInfo beHittedInfo)
        {
                if (!CanBeHit())
                {
                     beHittedInfo = BeHittedInfo.Default;
                     return;   
                }
                handler.BroadcastEvent(PlayerEvent.BeHitted,EEntityEventScope.Component,new PlayerEvent.BeHittedEventArgs(damageInfo));
                if (handler.TryGetComponet(EntityComponetUsage.Health,out PlayerHealth health))
                {
                        health.Hit(damageInfo,out beHittedInfo);
                        return;
                }      
                base.Hit(damageInfo, out beHittedInfo);
        }
}