using ilsFramework;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using Utils;

namespace Editor
{
    [CustomEditor(typeof(DamageTileClip))]
    public class DamageTileClipEditor : OdinEditor
    {
        protected override void OnEnable()
        {
            if (this.CheckTimelineClipIsSelected(out var clip) &&TimelineEditor.inspectedDirector)
            {
                if (TimelineEditor.inspectedDirector.GetGenericBinding(clip.GetParentTrack()) is BaseTileHandler handler)
                {
                    // 检测是否发生修改
                    foreach (var info in (target as DamageTileClip).AreaInfo)
                    {
                        TrackSceneOrInsperctorManager.Instance.TrySetDrawerPivotTransfrom_Clip(info.areaShape,handler.transform,target);
                        TrackSceneOrInsperctorManager.Instance.SetDrawerSceneVisbale(info.areaShape,true);
                    }
                }
            }
        }
        protected override void OnDisable()
        {
            foreach (var info in (target as DamageTileClip).AreaInfo)
            {
                TrackSceneOrInsperctorManager.Instance.SetDrawerSceneVisbale(info.areaShape,false);
            }
            base.OnDisable();
        }
    }
}