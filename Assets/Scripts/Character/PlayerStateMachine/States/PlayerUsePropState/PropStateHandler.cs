
using System;
using UnityEngine.Timeline;

/// <summary>
/// 用这个类来控制PlayerUsePropState这个大类
/// </summary>
public class PropStateHandler
{
    public Action<Type> onOrderToChangePlayerState;
    
    public Action<TimelineAsset> onOrderToPlayTimelineAsset;

    public Action<BaseProp> onRemoveProp;
    public void ChangePlayerState<T>() where T : IPlayerState
    { 
        onOrderToChangePlayerState?.Invoke(typeof(T));
    }

    public void PlayTimelineAsset(TimelineAsset timelineAsset)
    {
        onOrderToPlayTimelineAsset?.Invoke(timelineAsset);
    }

    public void RemoveThisProp(BaseProp prop)
    {
        onRemoveProp?.Invoke(prop);
    }
}