using System.Collections.Generic;
using AreaInfos.Shapes;
using ilsFramework;
using UnityEngine.Playables;

public class AreaEffectPlayableBehaviour : PlayableBehaviour
{
    public List<AreaInfo> AreaInfo;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        //每帧实现效果
        foreach (var areaInfo in AreaInfo)
        {
            (areaInfo.areaShape as CapsuleShape)?.angle.LogSelf();
        }
    }
}