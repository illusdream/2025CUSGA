using System;
using DefaultNamespace;
using ilsFramework;
using UnityEngine;

namespace Props
{
    public class BlackHoleGOController : EntityComponent
    {
        public const string Usage = "BlackHoleGOController";

        public override string TargetUsage => Usage;


        private TimerCollection timerCollection;
        
        private BlackHoleCannonPropConfig config;

        private Vector2 moveDir;
        
        public CommenActionDirector Director;
        public void Start()
        {
            timerCollection = new TimerCollection();

            if (PropManager.Instance.TryGetPropConfig(typeof(BlackHoleCannonProp),out  config))
            {
                timerCollection.CreateTimer(config.blackHoleEffectStartTime, 1,"BlackHoleExplosion")
                    .SetOnFinish(OnBlackHoleStartEffect)
                    .SetOnCycling(OnBlackHoleFlyingEffect)
                    .Register();
            }

            if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove move))
            {
                moveDir = move.GetEntityVelocity().normalized;
            }
        }

        private void OnBlackHoleFlyingEffect(Timer timer)
        {
            if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove move))
            {
                move.SetTargetVelocity(moveDir * (config.blackHoleSpeed * (1-timer.Progress)));
            }
        }
        
        private void OnBlackHoleStartEffect(Timer timer)
        {
            Director.Play(config.blackHoleEffectTimeline);
            Director.onStopped+= DirectorOnonStopped;
            if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove move))
            {
                move.SetTargetVelocity(Vector3.zero);
            }
        }

        private void DirectorOnonStopped(BaseActionDirector obj)
        {
            Destroy(gameObject);
        }


        public void OnDestroy()
        {
            timerCollection.ClearAllTimers();
        }
    }
}