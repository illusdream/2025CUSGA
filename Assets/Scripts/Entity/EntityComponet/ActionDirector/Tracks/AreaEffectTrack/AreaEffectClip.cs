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
    public EAreaTargetType AreaTargetType;
    
    //接下来是Entity相关的
    [ValueDropdown("AllEntityType",IsUniqueList = true)]
    [ListDrawerSettings(DraggableItems = false, HideRemoveButton = true,ShowFoldout = false)]
    [ShowIf("AreaTargetType",EAreaTargetType.Entity)]
    public List<EEntityType> TargetEntityType;
    [ShowIf("AreaTargetType",EAreaTargetType.Entity)]
    public UnityEvent<HashSet<EntityHandler>> ApplyEffectToEntity;
    
    
    
    [ShowIf("AreaTargetType",EAreaTargetType.Tile)]
    public UnityEvent<HashSet<Vector2Int>> ApplyEffectToTile;
    
    
    public List<AreaInfo> AreaInfo;
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
        behaviour.AreaTargetType = AreaTargetType;
        behaviour.TargetEntityType = TargetEntityType;
        behaviour.ApplyEffectToEntity = ApplyEffectToEntity;
        behaviour.ApplyEffectToTile = ApplyEffectToTile;
        return playable;
    }

    public ClipCaps clipCaps => ClipCaps.None;
}