using ilsFramework;
using UnityEngine;
using UnityEngine.Playables;

namespace Test
{
    public class TestPlayableBehaviour : PlayableBehaviour
    {
        
        public Collider2D Collider;
        public int testInt;
        private int FrameCount;
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            FrameCount = 0;

            base.OnBehaviourPlay(playable, info);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {

            base.ProcessFrame(playable, info, playerData);
            
            //返回第一帧

        }
        
        
    }
}