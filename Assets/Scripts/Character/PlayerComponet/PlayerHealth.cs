using System;
using ilsFramework;

public class PlayerHealth : BaseHealthComponent
{
        public override void OnInitialized(EntityHandler handler)
        {
                AddEventListener(PlayerEvent.BeHitted,EEntityEventScope.Component,Listener_BeHitted);
                base.OnInitialized(handler);
        }

        public override void OnEntityDestroy(EntityHandler handler)
        {
                RemoveEventListener(PlayerEvent.BeHitted,EEntityEventScope.Component,Listener_BeHitted);
                base.OnEntityDestroy(handler);
        }

        private void Listener_BeHitted(EventArgs _args)
        {
                if (_args is PlayerEvent.BeHittedEventArgs args)
                {
                        Hit(args.DamageInfo,out var _);
                }
        }
}