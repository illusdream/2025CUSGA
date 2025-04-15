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
            SceneView.duringSceneGui += SceneViewOnduringSceneGui;
        }

        private void SceneViewOnduringSceneGui(SceneView obj)
        {
            if (this.CheckTimelineClipIsSelected(out var clip) &&TimelineEditor.inspectedDirector)
            {

                if (TimelineEditor.inspectedDirector.GetGenericBinding(clip.GetParentTrack()) is Transform go)
                {
                    // 检测是否发生修改
                    foreach (var info in (target as AreaEffectClip).AreaInfo)
                    {
                        info.areaShape.OnSceneGUI(go,target);
                    }
                }
                else
                {
  
                }
                Repaint();
            }

        }
    
        public void OnSceneGUI()
        {

        }
   

        
    }
}