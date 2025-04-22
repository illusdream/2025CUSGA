using System.Collections.Generic;
using AreaInfos.Shapes;
using DefaultNamespace;
using ilsFramework;

namespace Props
{
    public class AcceleratingFieldGOController : EntityComponent,IAreaEffectProcessEntity
    {
        public const string Usage = "AcceleratingFieldGOController";

        public override string TargetUsage => Usage;
        public CommenActionDirector Director;
        
        public void Start()
        {
            if (PropManager.Instance.TryGetPropConfig(typeof(AcceleratingFieldProp),out AcceleratingFieldPropConfig config))
            {
                Director.Play(config.AcceleratingFieldTimelineAsset);
                Director.onStopped += DirectorOnonStopped;
            }
        }

        private void DirectorOnonStopped(BaseActionDirector obj)
        {
            Destroy(gameObject);
        }

        public void ProcessEntity(HashSet<EntityHandler> findEntity)
        {
            foreach (var entityHandler in findEntity)
            {
                if (entityHandler.TryGetComponet(EntityComponetUsage.Buff,out BaseBuffContainer baseBuffContainer))
                {
                    baseBuffContainer.AddBuff(EBuffType.AcceleratingFieldBuff);
                }
            }
        }
    }
}