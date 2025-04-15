using System;
using ilsFramework;
using UnityEditor;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace AreaInfos.Shapes
{
    [Serializable]
    public class BoxShape : AreaShape
    {
        public Vector2 point;
        public Vector2 size ;
        public float angle;
        public BoxShape(Vector2 point, Vector2 size, float angle)
        {
            this.point = point;
            this.size = size;
            this.angle = angle;
        }

        public void GetCurrentData(Transform transform, out Vector2 point, out Vector2 size, out float angle)
        {
            point = transform.TransformPoint(this.point);
            size = this.size * transform.lossyScale;
            angle = transform.rotation.eulerAngles.z + this.angle * Mathf.Rad2Deg;
        }
        

        public override void OnSceneGUI(Transform areaPivotTransform,Object clip)
        {
#if UNITY_EDITOR
            Handles.color = Handles.UIColliderHandleColor;
            var worldPos = (Vector2)areaPivotTransform.TransformPoint(point);
            var oldSize = size * new Vector2(areaPivotTransform.lossyScale.x, areaPivotTransform.lossyScale.y);
            var cangle = angle+ areaPivotTransform.rotation.eulerAngles.z * Mathf.Deg2Rad;

            var lefttop = worldPos + (oldSize / 2f * new Vector2(-1, 1)).Rotate(cangle);
            var righttop = worldPos + (oldSize / 2f * new Vector2(1, 1)).Rotate(cangle);
            var leftbottom = worldPos + (oldSize / 2f * new Vector2(-1, -1)).Rotate(cangle);
            var rightbottom = worldPos + (oldSize / 2f * new Vector2(1, -1)).Rotate(cangle);
            
            //获取轴
            var x = Vector2.left.Rotate(cangle);
            var y = Vector2.up.Rotate(cangle);
            
            Handles.DrawLine(lefttop,righttop);
            Handles.DrawLine(righttop,rightbottom);
            Handles.DrawLine(rightbottom,leftbottom);
            Handles.DrawLine(leftbottom,lefttop);

            var oldLeftCenter = worldPos - (oldSize / 2f * Vector2.left).Rotate(cangle);
            var oldTopCenter = worldPos + (oldSize / 2f * Vector2.up).Rotate(cangle);
            
            var handleCapScale = HandleUtility.GetHandleSize(worldPos);
            var leftCenter = Handles.FreeMoveHandle(oldLeftCenter, 0.075f *handleCapScale, Vector3.left, Handles.CubeHandleCap);

            var rightCenter = Handles.FreeMoveHandle(worldPos + (oldSize / 2f * Vector2.left).Rotate(cangle), 0.075f *handleCapScale, -Vector3.left, Handles.CubeHandleCap);

            var topCenter = Handles.FreeMoveHandle(oldTopCenter, 0.075f *handleCapScale, Vector3.up, Handles.CubeHandleCap);

            var bottomCenter = Handles.FreeMoveHandle(worldPos - (oldSize / 2f * Vector2.up).Rotate(cangle), 0.075f *handleCapScale, -Vector3.up, Handles.CubeHandleCap);

            
            //获取x长度
            var width = Mathf.Max(Vector2.Dot((rightCenter - leftCenter), x),0)/x.magnitude;
            var heigth = Mathf.Max(Vector2.Dot((topCenter - bottomCenter), y),0)/y.magnitude;
            var newSize = new Vector2(width, heigth);
            var sub = newSize - oldSize;
            var center =worldPos + (sub* new Vector2(oldLeftCenter.Vec3_xy() == leftCenter ? -1:1,oldTopCenter.Vec3_xy() == topCenter ? -1:1)).Rotate(cangle)/2f ;

            point = areaPivotTransform.InverseTransformPoint(center);
            size = new Vector2(width,heigth) /new Vector2(areaPivotTransform.lossyScale.x, areaPivotTransform.lossyScale.y);
#endif
        }


    }
}