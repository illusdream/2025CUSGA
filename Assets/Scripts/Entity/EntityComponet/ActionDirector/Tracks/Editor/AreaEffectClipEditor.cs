using System;
using System.Linq;
using EditorUtils;
using ilsFramework;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using Utils;

namespace Editor
{
    [CustomEditor(typeof(AreaEffectClip))]
    public class AreaEffectClipEditor : OdinEditor
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
                if (target is AreaEffectClip _clip)
                {
                    foreach (var info in _clip.AreaInfo)
                    {
                        TrackSceneOrInsperctorManager.Instance.TrySetDrawerPivotTransfrom_Clip(info.areaShape,_clip.PivotTrasform.Resolve(resolver),target);
                        TrackSceneOrInsperctorManager.Instance.SetDrawerSceneVisbale(info.areaShape,true);   
                    }                     
                }
            }
        }
        protected override void OnDisable()
        {
            foreach (var info in (target as AreaEffectClip).AreaInfo)
            {
                TrackSceneOrInsperctorManager.Instance.SetDrawerSceneVisbale(info.areaShape,false);
            }
            base.OnDisable();
        }

        
    }
}