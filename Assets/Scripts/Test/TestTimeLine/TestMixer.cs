using System.Collections.Generic;
using ilsFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Test
{
    public class TestMixer : PlayableBehaviour
    {
        public List<EntityHandler> entityHandlers = new List<EntityHandler>();

        private double TimeRun;
        private int FrameCount;
        private int prepareFrameCount;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            FrameCount = 0;
            prepareFrameCount = 0;
            base.OnBehaviourPlay(playable, info);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (!EditorApplication.isPlaying)
            {
                return;
            }

            TimeRun += info.deltaTime;

            entityHandlers.Clear();

            base.ProcessFrame(playable, info, playerData);
        }

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            prepareFrameCount++;
           // $"prepareFrameCount:{prepareFrameCount},FixedCount:{Time.fixedTime}".LogSelf();
            base.PrepareFrame(playable, info);
        }
        
  
    }
}