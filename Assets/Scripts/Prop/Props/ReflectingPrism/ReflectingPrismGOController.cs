using System;
using System.Collections.Generic;
using AreaInfos.Shapes;
using DefaultNamespace;
using ilsFramework;
using UnityEngine;

namespace Props
{
    public class ReflectingPrismGOController : EntityComponent,IAreaEffectProcess
    {
        public const string Usage = "ReflectingPrismGOController";

        public override string TargetUsage => Usage;

        public CommenActionDirector Director;

        HashSet<EntityHandler> results = new HashSet<EntityHandler>();
        public void Start()
        {
            if (PropManager.Instance.TryGetPropConfig(typeof(ReflectingPrismProp),out ReflectingPrismPropConfig config))
            {
                Director.Play(config.ReflectingPrismTimelineAsset);
                Director.onStopped += DirectorOnonStopped;
            }
        }

        private void DirectorOnonStopped(BaseActionDirector obj)
        {
            Destroy(gameObject);
        }

        public void Process(List<AreaInfo> areas, Transform pivot,List<EEntityType> types)
        {
            foreach (var areaInfo in areas)
            {
                results.Clear();
   
                if (areaInfo.areaShape is RayShape rayShape)
                {
                    areaInfo.FindTargetInEntity(pivot, types,results);
                    rayShape.GetCurrentData(pivot,out var start,out var end);
                    ProcessEachLine(start,end,results);
                }

            }
        }

        private void ProcessEachLine(Vector2 startPoint, Vector2 endPoint, HashSet<EntityHandler> needProcess)
        {
            var normal = (endPoint - startPoint).normalized;
            normal = new Vector2(-normal.y,normal.x);
            foreach (var result in needProcess)
            {
                if (result.HasEntityTag(EEntityTags.DontEffectByReflectPrism))
                {
                    continue;
                }
                
                if (result.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove move))
                {
                    var vel = move.GetEntityVelocity();
                    var final = Vector2.Reflect(vel, normal);
                    move.SetTargetVelocity(final);
                }
            }
        }
    }
}