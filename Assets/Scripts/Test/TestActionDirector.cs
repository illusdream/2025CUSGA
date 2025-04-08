using System;
using System.Collections.Generic;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Test
{
    public class TestActionDirector : MonoBehaviour
    {
        public PlayableDirector playableDirector;

        public Collider2D binding;

        public static float loopFirstFrame;
        public static float loopFrameCount;
        
        private Dictionary<string,PlayableBinding> tracks = new Dictionary<string,PlayableBinding>();

        private void Start()
        {
            foreach (var bind in playableDirector.playableAsset.outputs)
            {
                if (!tracks.ContainsKey(bind.streamName))
                {
                    tracks.Add(bind.streamName, bind);
                }
            }


        }

        [Button]
        public void TestPlay(string trackName)
        {            


            if (tracks.TryGetValue(trackName, out var _binding))
            {
                _binding.outputTargetType.LogSelf();
                playableDirector.SetGenericBinding(_binding.sourceObject,binding);
            }
            playableDirector.Play();
            //playableDirector.SetGenericBinding();
        }
    }
}