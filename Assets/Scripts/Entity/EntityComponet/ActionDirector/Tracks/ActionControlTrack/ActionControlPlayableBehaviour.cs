using System;
using UnityEngine.Playables;

public class ActionControlPlayableBehaviour : PlayableBehaviour
{
        public BaseActionDirector ActionDirector;
        
        public int ClipIndex;
    
        public EControlClipType ControlClipType;

        /// <summary>
        /// 循环次数
        /// </summary>
        public int LoopCount;
        
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
                switch (ControlClipType)
                {
                        case EControlClipType.None:
                                break;
                        case EControlClipType.LoopByTimes:
                                //将对应设置传递给Handler
                                ActionDirector.ControlTrackHandler.SetClipType(ControlClipType,ClipIndex,LoopCount);
                                break;
                        case EControlClipType.LoopByCondition:
                                ActionDirector.ControlTrackHandler.SetClipType(ControlClipType,ClipIndex);
                                break;
                        default:
                                throw new ArgumentOutOfRangeException();
                }        
        }
}