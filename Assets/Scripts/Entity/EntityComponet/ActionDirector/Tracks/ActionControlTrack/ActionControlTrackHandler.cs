using System;
using System.Collections.Generic;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine.Timeline;

[Serializable]
public class ActionControlTrackHandler
{
        public EControlClipType clipType = EControlClipType.None;

        public int currentClipIndex = -1;
        
        public int LoopTime = -1;
        /// <summary>
        /// 是否在循环
        /// </summary>
        public bool Loop { get;private set; }
        
        Dictionary<int,TimelineClip> ControlClips = new Dictionary<int,TimelineClip>();
        /// <summary>
        /// 设置是否循环，这个只在<see cref="EControlClipType.LoopByCondition"/>有效
        /// </summary>
        /// <param name="loop"></param>
        [Button]
        public void SetLoop(bool loop)
        {
                this.Loop = loop;
        }

        public void SetClipType(EControlClipType _clipType, int clipIndex, int LoopTime = -1)
        {
                this.clipType = _clipType;
                switch (_clipType)
                {
                        case EControlClipType.None:
                                this.currentClipIndex = clipIndex;
                                break;
                        case EControlClipType.LoopByTimes:
                                if (clipIndex == currentClipIndex)
                                {
                                        return;
                                }
                                this.LoopTime = LoopTime-1;
                                this.currentClipIndex = clipIndex;
                                break;
                        case EControlClipType.LoopByCondition:
                                this.currentClipIndex = clipIndex;
                                Loop = true;
                                break;
                        default:
                                throw new ArgumentOutOfRangeException(nameof(_clipType), _clipType, null);
                }
        }

        public void GetNextFrameTime(float currentFrameTime,float nextFrameTime, out float cNextFrameTime)
        {
                cNextFrameTime = nextFrameTime;
                switch (clipType)
                {
                        case EControlClipType.None:
                                break;
                        case EControlClipType.LoopByTimes:
                        {
                                if (ControlClips.TryGetValue(currentClipIndex,out var clip))
                                {
                                        if(LoopTime > 0  && currentFrameTime >= clip.end)
                                        {
                                                cNextFrameTime = (float)clip.start;
                                                LoopTime--;
                                                return;
                                        }
                                }
                        }
                                return;
                        case EControlClipType.LoopByCondition:
                        {
                                if (ControlClips.TryGetValue(currentClipIndex,out var clip))
                                {
                                        if(Loop  && currentFrameTime >= clip.end)
                                        {
                                                cNextFrameTime = (float)clip.start;
                                                return;
                                        }
                                }
                        }
                                return;
                        default:
                                throw new ArgumentOutOfRangeException();
                }

        }

        public void Reset(TimelineAsset timelineAsset)
        {
                ControlClips.Clear();
                foreach (var track in timelineAsset.GetOutputTracks())
                {
                        if (track is ActionControlTrack tt)
                        {
                                foreach (var timelineClip in tt.GetClips() )
                                {
                                        if (timelineClip.asset is ActionControlClip acClip)
                                        {
                                                ControlClips[acClip.ClipIndex] = timelineClip;
                                        }
                                }
                        }
                }
                LoopTime = 0;
                this.clipType = EControlClipType.None;
                this.currentClipIndex = -1;
        }
}