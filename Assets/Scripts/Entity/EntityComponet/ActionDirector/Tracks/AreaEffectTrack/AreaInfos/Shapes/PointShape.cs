using System;
using ilsFramework;
using Sirenix.OdinInspector;
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
            //额外绘制一个点，用于显示
            Handles.DrawSolidRectangleWithOutline(new Rect(areaPivotTransform.TransformPoint(point),Vector2.one*0.3f),Color.green, Color.white);
            
            EditorGUI.BeginChangeCheck();
            var newPosition = Handles.PositionHandle(areaPivotTransform.TransformPoint(point), areaPivotTransform.rotation);
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