using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Test
{
    public class TestClip : PlayableAsset, ITimelineClipAsset
    {
        public TestPlayableBehaviour template = new TestPlayableBehaviour();
        
        public ExposedReference<Collider2D> exampleValue;
        public int TestInt;
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<TestPlayableBehaviour>.Create(graph,template);
            TestPlayableBehaviour behaviour = playable.GetBehaviour();
            behaviour.Collider = exampleValue.Resolve(graph.GetResolver());
            behaviour.testInt = TestInt;
            return playable;
        }
        [SerializeField]
        [ShowInInspector]
        public ClipCaps clipCaps => ClipCaps.Looping |  ClipCaps.None;
    }
}