using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackBindingType(typeof(BaseProp))]
[TrackClipType(typeof(PropUsingClip))]
[TrackColor(1,1,1)]
public class PropUsingTrack : PlayableTrack,ILayerable
{
        public override bool CanCreateTrackMixer()
        {
               
                return true;
                
        }

        public Playable CreateLayerMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<PropUsingMixer>.Create(graph, inputCount);
        }
}

public class PropUsingMixer : PlayableBehaviour
{
    
}
