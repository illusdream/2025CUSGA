using System;
using System.Collections.Generic;
using AreaInfos.Shapes;
using ilsFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class AreaEffectPlayableBehaviour : PlayableBehaviour
{
    public List<AreaInfo> AreaInfos;
    
    //接下来是Entity相关的
    public List<EEntityType> TargetEntityType;
    
    public HashSet<EntityHandler> findEntities = new HashSet<EntityHandler>();
    
    public HashSet<Vector2Int> findPositions = new HashSet<Vector2Int>();
    
    public ExposedReference<Transform> PivotTransform;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
#if UNITY_EDITOR
        if(!EditorApplication.isPlaying)
            return;
#endif
        var transform = PivotTransform.Resolve(playable.GetGraph().GetResolver());

        var script = playerData;
        
        if (script is IAreaEffectProcessEntity areaEffectProcessEntity)
        {
            findEntities.Clear();
            foreach (var areaInfo in AreaInfos)
            {
                areaInfo.FindTargetInEntity(transform,TargetEntityType,findEntities);
            }
            areaEffectProcessEntity.ProcessEntity(findEntities);
        }
        
        if (script is IAreaEffectProcessTile areaEffectProcessTile)
        {
            findPositions.Clear();
            foreach (var areaInfo in AreaInfos)
            {
                areaInfo.FindTargetInTile(transform,findPositions);
            }
            areaEffectProcessTile.ProcessTile(findPositions);
        }
        
        if (script is IAreaEffectProcess areaEffectProcess)
        {
            areaEffectProcess.Process(AreaInfos,transform,TargetEntityType);
        }
    }
}