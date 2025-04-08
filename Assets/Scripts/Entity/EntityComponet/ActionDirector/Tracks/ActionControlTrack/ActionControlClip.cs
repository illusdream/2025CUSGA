using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ActionControlClip : PlayableAsset,ITimelineClipAsset
{
    private ActionControlPlayableBehaviour template = new ActionControlPlayableBehaviour();
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<ActionControlPlayableBehaviour>.Create(graph,template);
        ActionControlPlayableBehaviour behaviour = playable.GetBehaviour();
        behaviour.ActionDirector = ActionDirector.Resolve(graph.GetResolver());
        behaviour.ClipIndex = ClipIndex;
        behaviour.ControlClipType = ControlClipType;
        behaviour.LoopCount = LoopCount;
        return playable;
    }

    public ClipCaps clipCaps => ClipCaps.None;


    public ExposedReference<BaseActionDirector> ActionDirector;

    public int ClipIndex;
    
    public EControlClipType ControlClipType;

    /// <summary>
    /// 循环次数
    /// </summary>
    public int LoopCount;
}