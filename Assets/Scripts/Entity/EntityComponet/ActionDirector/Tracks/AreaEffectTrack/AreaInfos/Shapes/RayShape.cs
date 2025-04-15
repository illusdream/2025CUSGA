using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Internal;
using Utils;
using Object = UnityEngine.Object;

namespace AreaInfos.Shapes
{
    [Serializable]
    public class RayShape : AreaShape
    {

        public Vector2 start;
        public Vector2 end;

        public RayShape(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }
        public void GetCurrentData(Transform transform, out Vector2 start, out Vector2 end)
        {
            start  = transform.TransformPoint(this.start);
            end  = transform.TransformPoint(this.end);
        }

        public override void OnSceneGUI(Transform areaPivotTransform,Object clip)
        {
#if UNITY_EDITOR
            var pos = areaPivotTransform.TransformPoint(start);
            var pos2 = areaPivotTransform.TransformPoint(end);
            TimelineEditorUtils.DrawGreenDotIcon(pos, Vector2.one *64);
            TimelineEditorUtils.DrawGreenDotIcon(pos2, Vector2.one *64);
            
            Handles.DrawDottedLine(pos, pos2,1);
            EditorGUI.BeginChangeCheck();
            
            
            var newPosition = Handles.PositionHandle(pos, areaPivotTransform.rotation);
            // 检测是否发生修改
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(clip, "Move Object"); // 记录Transform的修改
                EditorUtility.SetDirty(clip);
                this.start = areaPivotTransform.InverseTransformPoint(newPosition); // 应用新位置
            }
            
            var newPosition2 = Handles.PositionHandle(pos2, areaPivotTransform.rotation);
            // 检测是否发生修改
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(clip, "Move Object"); // 记录Transform的修改
                EditorUtility.SetDirty(clip);
                this.end = areaPivotTransform.InverseTransformPoint(newPosition2); // 应用新位置
            }
#endif
        }

    }
}