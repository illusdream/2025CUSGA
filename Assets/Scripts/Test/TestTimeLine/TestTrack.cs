using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Test
{
    [Serializable]
    [TrackClipType(typeof(TestClip))]
    [TrackColor(0.53f,0.0f,0.08f)]
    public class TestTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var mixplable = ScriptPlayable<TestMixer>.Create(graph);
            mixplable.SetInputCount(inputCount);
            return mixplable;
        }
    }
}