using System;
using System.Collections.Generic;
using AreaInfos.Shapes;
using ilsFramework;
using UnityEngine;

namespace Props
{
    public class ToxicGas : EntityComponent
    {
        public const string Usage = "ToxicGas";

        public override string TargetUsage => Usage;

        public CircleShape effectShape;

        public List<EEntityType> effectTypes;

        public HashSet<EntityHandler> effectResult;

        private TimerCollection TimerCollection;
        public void Start()
        {
            TimerCollection = new TimerCollection();
            if (PropManager.Instance.TryGetPropConfig(typeof(GasCanisterProp),out GasCanisterPropConfig propConfig))
            {
                TimerCollection.CreateTimer(propConfig.ToxicGasTime,1,"ToxicGas").SetOnFinish(KillSelf).Register();
            }
            effectResult = new HashSet<EntityHandler>();
        }

        public void FixedUpdate()
        {
            effectResult.Clear();
            EntityManager.Instance.GetEntityOverlapByShape(transform, effectShape, effectTypes, effectResult);

            foreach (var entityHandler in effectResult)
            {
                if (entityHandler.ID == handler.SpawnSource.SpawnerID)
                {
                    continue;
                }
                if (entityHandler.TryGetComponet(EntityComponetUsage.Buff,out BaseBuffContainer container))
                {
                    container.AddBuff(EBuffType.PoisoningBuff);
                }
            }
        }

        private void KillSelf(Timer timer)
        {
            Destroy(gameObject);
        }

        public void OnDestroy()
        {
            TimerCollection.ClearAllTimers();
        }
    }
}