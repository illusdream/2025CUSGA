using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AreaEffectClip : PlayableAsset,ITimelineClipAsset
{
    //控制器
    //应该要一个pivot来选定区域

    
    public List<AreaInfo> AreaInfo;
    
    private AreaEffectPlayableBehaviour template = new AreaEffectPlayableBehaviour();
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<AreaEffectPlayableBehaviour>.Create(graph,template);
        AreaEffectPlayableBehaviour behaviour = playable.GetBehaviour();
        behaviour.AreaInfo = AreaInfo;
        return playable;
    }

    public ClipCaps clipCaps => ClipCaps.None;
}