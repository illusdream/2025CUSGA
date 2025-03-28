using System.Collections.Generic;
using ilsFramework;
using UnityEditor;
using UnityEngine.Playables;

namespace Test
{
    public class TestMixer : PlayableBehaviour
    {
        public List<EntityHandler> entityHandlers = new List<EntityHandler>();
        
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (!EditorApplication.isPlaying)
            {
                return;
            }
            entityHandlers.Clear();
            for(int i = 0 ; i < playable.GetInputCount(); i++)//获取轨道上所有的片段
            {
                float weight  = playable.GetInputWeight(i);//获取片段在当前帧的片段
                var clipPlayable = (ScriptPlayable<TestPlayableBehaviour>)playable.GetInput(i);
                TestPlayableBehaviour behaviour = clipPlayable.GetBehaviour();//获取CustomPlayableBehaviour
                EntityManager.Instance.GetEntityInArea(behaviour.Collider,EEntityType.Character,entityHandlers);

                foreach (var handler in entityHandlers)
                {
                    handler.ID.LogSelf();
                }
            }
            base.ProcessFrame(playable, info, playerData);
        }
    }
}