using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AreaInfos.Shapes
{
    [Serializable]
    public class CircleShape : AreaShape
    {
        public Vector2 point;
        public float radius;

        public CircleShape(Vector2 point, float radius)
        {
            this.point = point;
            this.radius = radius;
        }

        public void GetCurrentData(Transform transform, out Vector2 point, out float radius)
        {
            var arvgScale = (transform.lossyScale.x + transform.lossyScale.y) / 2;
            point  = transform.TransformPoint(this.point);
             radius = this.radius * arvgScale;
        }

        public override void OnSceneGUI(Transform areaPivotTransform,Object clip)
        {
#if UNITY_EDITOR
            var arvgScale = (areaPivotTransform.lossyScale.x + areaPivotTransform.lossyScale.y) / 2;
            var _center = areaPivotTransform.TransformPoint(point);
            var _radius = radius * arvgScale;
            
            Handles.color = new Color(0,1,0,0.03f);
            Handles.DrawSolidDisc(_center, Vector3.forward, _radius);

            
            // 调整圆心
            EditorGUI.BeginChangeCheck();
            Vector3 newCenter = Handles.PositionHandle(_center, areaPivotTransform.rotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(clip, "Move Circle Center");
                point = areaPivotTransform.InverseTransformPoint(newCenter);
            }

            // 调整半径
            Handles.color = new Color(0,1,0,0.2f);
            float newRadius = Handles.RadiusHandle(areaPivotTransform.rotation, _center, _radius);
            if (Mathf.Abs(newRadius - _radius)/arvgScale > 0.01f)
            {

                radius = newRadius/arvgScale;
                EditorUtility.SetDirty(clip);
            }
#endif
        }

    }
}