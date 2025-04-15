using System;
using ilsFramework;
using UnityEngine.Playables;

public class ActionControlPlayableBehaviour : PlayableBehaviour
{
        public int ClipIndex;
    
        public EControlClipType ControlClipType;

        public bool Init = false;
        
        /// <summary>
        /// 循环次数
        /// </summary>
        public int LoopCount;
        
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
                Init = true;
        }

        public override void OnGraphStart(Playable playable)
        {
                base.OnGraphStart(playable);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
                if (Init)
                {
                        Init = false;
                        BaseActionDirector ActionDirector = (BaseActionDirector)playerData;
                        switch (ControlClipType)
                        {
                                case EControlClipType.None:
                                        ActionDirector?.ControlTrackHandler.SetClipType(ControlClipType,ClipIndex);
                                        break;
                                case EControlClipType.LoopByTimes:
                                        //将对应设置传递给Handler
                                        ActionDirector?.ControlTrackHandler.SetClipType(ControlClipType,ClipIndex,LoopCount);
                                        break;
                                case EControlClipType.LoopByCondition:
                                        ActionDirector?.ControlTrackHandler.SetClipType(ControlClipType,ClipIndex);
                                        break;
                                default:
                                        throw new ArgumentOutOfRangeException();
                        }   
                }
                base.ProcessFrame(playable, info, playerData);
        }
}