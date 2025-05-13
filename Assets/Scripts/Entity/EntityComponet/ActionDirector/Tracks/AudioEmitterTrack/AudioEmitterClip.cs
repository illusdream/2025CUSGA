using System.Collections.Generic;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AudioEmitterClip: PlayableAsset,ITimelineClipAsset
{
    
    [ValueDropdown( "GetAllAudioChannelNames")]
    public string OutputAudioChannel;
    
    public SoundData soundData;

    public bool ShouldControllPlay;
    
    private AudioEmitterPlayableBehaviour template = new AudioEmitterPlayableBehaviour();
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<AudioEmitterPlayableBehaviour>.Create(graph,template);
        AudioEmitterPlayableBehaviour behaviour = playable.GetBehaviour();
        behaviour.soundData = soundData;
        behaviour.OutputAudioChannel = OutputAudioChannel;
        behaviour.ShouldControllPlay = ShouldControllPlay;
        return playable;
    }

    public ClipCaps clipCaps => ClipCaps.None;

    public List<string> GetAllAudioChannelNames()
    {
        return new List<string>() {AudioChannelName.BGM,AudioChannelName.Sound};
    }
}