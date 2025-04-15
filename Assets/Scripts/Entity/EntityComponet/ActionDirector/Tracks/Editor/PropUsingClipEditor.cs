using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Timeline;
using Utils;

namespace Editor
{
    [CustomEditor(typeof(PropUsingClip))]
    public class PropUsingClipEditor : OdinEditor
    {
        protected override void OnEnable()
        {
            if (this.CheckTimelineClipIsSelected(out var clip) &&TimelineEditor.inspectedDirector)
            {
                var resolver = TimelineEditor.inspectedDirector.playableGraph.GetResolver();
                if (resolver == null)
                {
                    return;
                }
                // 检测是否发生修改
                if ((target as PropUsingClip).Setter is IPropHasAreaInfo info)
                {
                    foreach (var shape in info.GetAllAreaShapes())
                    {
                        TrackSceneOrInsperctorManager.Instance.TrySetDrawerPivotTransfrom_Clip(shape.Item1,
                            shape.Item2.Resolve(resolver), target);
                        TrackSceneOrInsperctorManager.Instance.SetDrawerSceneVisbale(shape.Item1,true);
                    }
                }
            }
        }
        protected override void OnDisable()
        {
            if ((target as PropUsingClip).Setter is IPropHasAreaInfo info)
            {
                foreach (var shape in info.GetAllAreaShapes())
                {
                    TrackSceneOrInsperctorManager.Instance.SetDrawerSceneVisbale(shape.Item1,false);
                }
            }
            //TrackSceneOrInsperctorManager.Instance.SetDrawerSceneVisbale(AreaShape,false);
            base.OnDisable();
        }
    }
}