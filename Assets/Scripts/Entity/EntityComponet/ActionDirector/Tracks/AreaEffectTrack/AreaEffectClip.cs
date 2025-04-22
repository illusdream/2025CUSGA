using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class AreaEffectClip : PlayableAsset,ITimelineClipAsset
{
    //接下来是Entity相关的
    [ValueDropdown("AllEntityType",IsUniqueList = true)]
    [ListDrawerSettings(DraggableItems = false, HideRemoveButton = true,ShowFoldout = false)]
    public List<EEntityType> TargetEntityType;
    
    public List<AreaInfo> AreaInfo;
    
    public ExposedReference<Transform> PivotTrasform;
    public  List<EEntityType> AllEntityType()
    {
        return Enum.GetValues(typeof(EEntityType)).OfType<EEntityType>().ToList();
    }
    
    private AreaEffectPlayableBehaviour template = new AreaEffectPlayableBehaviour();
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<AreaEffectPlayableBehaviour>.Create(graph,template);
        AreaEffectPlayableBehaviour behaviour = playable.GetBehaviour();
        behaviour.AreaInfos = AreaInfo;
        behaviour.TargetEntityType = TargetEntityType;
        behaviour.PivotTransform  = PivotTrasform;
        return playable;
    }

    public ClipCaps clipCaps => ClipCaps.None;
}