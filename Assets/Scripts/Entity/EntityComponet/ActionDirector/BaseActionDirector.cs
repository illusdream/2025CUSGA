using System;
using System.Collections.Generic;
using ilsFramework;
using Sirenix.OdinInspector;
using Test;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Object = UnityEngine.Object;

[RequireComponent(typeof(PlayableDirector))]
public abstract class BaseActionDirector : EntityComponent
{
        public sealed override string TargetUsage => EntityComponetUsage.ActionDirector;

        /// <summary>
        /// 对应的Timeline播放器
        /// </summary>
        public PlayableDirector director;

        public bool isPlaying {get;private set;}

        public event Action<BaseActionDirector> onStopped;
        
        public event Action<BaseActionDirector> onStarted;

        public event Action<BaseActionDirector> onAction;

        private float directorTime;
        /// <summary>
        /// 缓存对应名字的实例
        /// </summary>
        public Dictionary<string, Object> bindingBuffer;
        [ShowInInspector]
        public ActionControlTrackHandler ControlTrackHandler { get;private set; }
        

        public virtual  void Awake()
        {
                bindingBuffer ??= new Dictionary<string, Object>();
                ControlTrackHandler = new ActionControlTrackHandler();
        }

        public virtual void Start()
        {
                director.timeUpdateMode = DirectorUpdateMode.Manual;
        }

        public virtual  void FixedUpdate()
        {
                if (directorTime >= director.duration && isPlaying)
                {
                        isPlaying = false;
                        onStopped?.Invoke(this);
                        return;      
                }
                //在这个里面更新Director吧，固定帧数
                if (isPlaying)
                {
                        ControlTrackHandler.GetNextFrameTime(directorTime,directorTime + Time.fixedDeltaTime,out directorTime);
                        director.time = directorTime;
                        director.Evaluate();
                        onAction?.Invoke(this);
                }

        }

        public void Update()
        {

        }

        /// <summary>
        /// 是否可以播放一个动作片段（timeline）
        /// </summary>
        /// <returns></returns>
        public abstract bool CanPlay();

        /// <summary>
        /// 尝试播放一个timeline（动作片段）
        /// </summary>
        /// <param name="timelineAsset"></param>
        /// <returns></returns>
        public virtual  bool TryPlay(TimelineAsset timelineAsset)
        {
                if (CanPlay())
                {
                        Play(timelineAsset);
                        return true;
                }
                return false;
        }

        /// <summary>
        /// 播放一个timeline（动作片段）
        /// </summary>
        /// <param name="timelineAsset"></param>
        [Button]
        public virtual  void Play(TimelineAsset timelineAsset)
        {
                ControlTrackHandler.Reset(timelineAsset);
                directorTime = 0;
                isPlaying = true;
                onStarted?.Invoke(this);
                director.Play(timelineAsset);
        }



        /// <summary>
        /// 停止当前正在播放的片段
        /// </summary>
        public void Pause()
        {
                director.Pause();
        }

}