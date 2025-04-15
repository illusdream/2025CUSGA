using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DamageTileClip : PlayableAsset,ITimelineClipAsset
{
    private DamageTilePlayableBehaviour template = new DamageTilePlayableBehaviour();
    
    public List<AreaInfo> AreaInfo;
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DamageTilePlayableBehaviour>.Create(graph,template);
        DamageTilePlayableBehaviour behaviour = playable.GetBehaviour();
        behaviour.areaInfos = AreaInfo;
        return playable;
    }

    public ClipCaps clipCaps => ClipCaps.None;
}