using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AttackEntityClip : PlayableAsset,ITimelineClipAsset
{
    private AttackEntityPlayableBehaviour template = new AttackEntityPlayableBehaviour();

    [ToggleLeft]
    public bool CanAttackSpawnerOrSpawnerOwner;
    
    public List<AreaInfo> AreaInfo;
    
    [ValueDropdown("AllEntityType",IsUniqueList = true)]
    [ListDrawerSettings(DraggableItems = false, HideRemoveButton = true,ShowFoldout = false)]
    public List<EEntityType> TargetEntityType;
    
    public  List<EEntityType> AllEntityType()
    {
        return Enum.GetValues(typeof(EEntityType)).OfType<EEntityType>().ToList();
    }
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<AttackEntityPlayableBehaviour>.Create(graph,template);
        AttackEntityPlayableBehaviour behaviour = playable.GetBehaviour();
        behaviour.AreaInfo = AreaInfo;
        behaviour.TargetEntityType = TargetEntityType;
        behaviour.CanAttackSpawnerOrSpawnerOwner = CanAttackSpawnerOrSpawnerOwner;
        return playable;
    }

    public ClipCaps clipCaps => ClipCaps.None;
}