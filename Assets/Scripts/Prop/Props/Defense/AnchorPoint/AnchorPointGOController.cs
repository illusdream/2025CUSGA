using System;
using System.Collections.Generic;
using AreaInfos.Shapes;
using DefaultNamespace;
using UnityEngine;

namespace Props
{
    public class AnchorPointGOController : EntityComponent,IAreaEffectProcess
    {
        public const string Usage = "AnchorPointGOController";

        public override string TargetUsage => Usage;
        
        public CommenActionDirector Director;


        public override void OnInitialized(EntityHandler handler)
        {

            base.OnInitialized(handler);
        }

        public void Start()
        {
            if (PropManager.Instance.TryGetPropConfig(typeof(AnchorPointProp),out AnchorPointPropConfig config))
            {
                Director.Play(config.anchorPointTimelineAsset);
                Director.onStopped += DirectorOnonStopped;
            }
        }

        private void DirectorOnonStopped(BaseActionDirector obj)
        {
            Destroy(gameObject);
        }
        
        public void Process(List<AreaInfo> areas, Transform pivot, List<EEntityType> types)
        {
            if (areas[0].areaShape is PointShape pointShape)
            {
                pointShape.GetCurrentData(pivot,out var point);

                var ID = handler.SpawnSource.SpawnerID;
                if (CharacterManager.Instance.TryGetPlayerController(ID,out var controller))
                {
                    controller.transform.position = point;
                }

            }
        }
    }
}