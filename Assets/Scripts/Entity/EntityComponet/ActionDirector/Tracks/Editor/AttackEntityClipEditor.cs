using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Timeline;
using Utils;

namespace Editor
{
    [CustomEditor(typeof(AttackEntityClip))]
    public class AttackEntityClipEditor : OdinEditor
    {
        protected override void OnEnable()
        {
            if (this.CheckTimelineClipIsSelected(out var clip) &&TimelineEditor.inspectedDirector)
            {
                if (TimelineEditor.inspectedDirector.GetGenericBinding(clip.GetParentTrack()) is BaseAttacker handler)
                {
                    foreach (var info in (target as AttackEntityClip).AreaInfo)
                    {
                        TrackSceneOrInsperctorManager.Instance.TrySetDrawerPivotTransfrom_Clip(info.areaShape,handler.transform,target);
                        TrackSceneOrInsperctorManager.Instance.SetDrawerSceneVisbale(info.areaShape,true);
                    }
                }
            }
        }
        protected override void OnDisable()
        {
            foreach (var info in (target as AttackEntityClip).AreaInfo)
            {
                TrackSceneOrInsperctorManager.Instance.SetDrawerSceneVisbale(info.areaShape,false);
            }
            base.OnDisable();
        }
    }
}