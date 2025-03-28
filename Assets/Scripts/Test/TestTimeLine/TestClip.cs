using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Test
{
    public class TestClip : PlayableAsset, ITimelineClipAsset
    {
        public TestPlayableBehaviour template = new TestPlayableBehaviour();
        
        public ExposedReference<Collider2D> exampleValue;
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<TestPlayableBehaviour>.Create(graph,template);
            TestPlayableBehaviour behaviour = playable.GetBehaviour();
            behaviour.Collider = exampleValue.Resolve(graph.GetResolver());
            
            return playable;
        }

        public ClipCaps clipCaps => ClipCaps.Blending;
    }
}