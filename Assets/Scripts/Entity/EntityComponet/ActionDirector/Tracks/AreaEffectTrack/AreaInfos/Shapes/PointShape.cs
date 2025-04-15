using System;
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
        public void GetCurrentData(Transform transform, out Vector2 point)
        {
            point  = transform.TransformPoint(this.point);
        }

        public override void OnSceneGUI(Transform areaPivotTransform,Object clip)
        {
#if UNITY_EDITOR
            var pos = areaPivotTransform.TransformPoint(point);
            //TimelineEditorUtils.DrawGreenDotIcon(pos, Vector2.one *64);
            EditorGUI.BeginChangeCheck();
            var newPosition = Handles.PositionHandle(pos, areaPivotTransform.rotation);
            // 检测是否发生修改
            if (EditorGUI.EndChangeCheck())
            {                                   

                this.point = areaPivotTransform.InverseTransformPoint(newPosition); // 应用新位置
                EditorUtility.SetDirty(clip);
            }
#endif
        }


    }
}