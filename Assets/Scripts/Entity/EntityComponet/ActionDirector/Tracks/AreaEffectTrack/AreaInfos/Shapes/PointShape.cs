using System;
using ilsFramework;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AreaInfos.Shapes
{
    [Serializable]
    public class PointShape : AreaShape
    {
        public Vector2 point;

        public PointShape(Vector2 point)
        {
            this.point = point;
        }
#if UNITY_EDITOR
        public override void OnSceneGUI(Transform areaPivotTransform,Object clip)
        {
            var pos = areaPivotTransform.TransformPoint(point);
            var size = Vector2.one *64/ HandleUtility.GetHandleSize(pos);
            
            Handles.BeginGUI();
            var icon  =EditorGUIUtility.IconContent("sv_icon_dot11_pix16_gizmo").image as Texture2D;
            Rect rect = new Rect().SetSize(size).SetCenter(HandleUtility.WorldToGUIPoint(pos));
            GUI.DrawTexture(rect, icon);
            Handles.EndGUI();
            EditorGUI.BeginChangeCheck();
            var newPosition = Handles.PositionHandle(pos, areaPivotTransform.rotation);
            // 检测是否发生修改
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(clip, "Move Object"); // 记录Transform的修改
                EditorUtility.SetDirty(clip);
                this.point = areaPivotTransform.InverseTransformPoint(newPosition); // 应用新位置
            }
        }
#endif
    }
}