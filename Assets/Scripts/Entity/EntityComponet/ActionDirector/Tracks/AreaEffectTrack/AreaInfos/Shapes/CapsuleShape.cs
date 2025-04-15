using System;
using ilsFramework;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace AreaInfos.Shapes
{
    [Serializable]
    public class CapsuleShape : AreaShape
    {
        public Vector2 point;
        public Vector2 size;
        public CapsuleDirection2D direction;
        public float angle;

        public CapsuleShape(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle)
        {
            this.point = point;
            this.size = size;
            this.direction = direction;
            this.angle = angle;
        }

        public void GetCurrentData(Transform transform, out Vector2 point, out Vector2 size,out CapsuleDirection2D direction,out float angle)
        {
            point = transform.TransformPoint(this.point);
            size = this.size * transform.lossyScale;
            direction = this.direction;
            angle = transform.rotation.eulerAngles.z + this.angle * Mathf.Rad2Deg;
        }
        

        public override void OnSceneGUI(Transform areaPivotTransform,Object clip)
        {
#if UNITY_EDITOR
            Handles.color = Handles.UIColliderHandleColor;
            
            var worldPos = (Vector2)areaPivotTransform.TransformPoint(point);
            var oldSize = size * new Vector2(areaPivotTransform.lossyScale.x, areaPivotTransform.lossyScale.y);
            var cangle = angle+ areaPivotTransform.rotation.eulerAngles.z * Mathf.Deg2Rad;

            //获取轴
            var x = Vector2.left.Rotate(cangle);
            var y = Vector2.up.Rotate(cangle);
            

            
            //圆半径
            var cirR = direction switch
            {
                CapsuleDirection2D.Vertical => oldSize.x/2,
                CapsuleDirection2D.Horizontal => oldSize.y/2,
                _ => 0
            };
            switch (direction)
            {
                case CapsuleDirection2D.Vertical:
                {
                    //上半圆
                    var upCirCenter = worldPos +( new Vector2(0,oldSize.y/2 - cirR)).Rotate(cangle);
                    Handles.DrawWireArc(upCirCenter,Vector3.forward,(Vector2.right*cirR).Rotate(cangle),Mathf.PI*Mathf.Rad2Deg,cirR);

                    //下半圆
                    var downCirCenter = worldPos -( new Vector2(0,oldSize.y/2 - cirR)).Rotate(cangle);
                    Handles.DrawWireArc(downCirCenter,Vector3.forward, (Vector2.right*cirR).Rotate(cangle),-Mathf.PI*Mathf.Rad2Deg,cirR);

                    var lefttop = worldPos + (new Vector2(-oldSize.x/2,oldSize.y/2 - cirR)).Rotate(cangle);
                    var righttop = worldPos + (new Vector2(oldSize.x/2,oldSize.y/2 - cirR)).Rotate(cangle);
                    var leftbottom = worldPos +(new Vector2(-oldSize.x/2,-(oldSize.y/2 - cirR))).Rotate(cangle);
                    var rightbottom = worldPos +(new Vector2(oldSize.x/2,-(oldSize.y/2 - cirR))).Rotate(cangle);
                    //连线
                    Handles.DrawLine(lefttop,leftbottom);
                    Handles.DrawLine(righttop,rightbottom);
                }
                    break;
                case CapsuleDirection2D.Horizontal:
                {
                    //上半圆
                    var upCirCenter = worldPos +( new Vector2(oldSize.x/2 - cirR,0)).Rotate(cangle);
                    Handles.DrawWireArc(upCirCenter,Vector3.forward,(Vector2.down*cirR).Rotate(cangle),Mathf.PI*Mathf.Rad2Deg,cirR);

                    //下半圆
                    var downCirCenter = worldPos -( new Vector2(oldSize.x/2 - cirR,0)).Rotate(cangle);
                    Handles.DrawWireArc(downCirCenter,Vector3.forward, (Vector2.down*cirR).Rotate(cangle),-Mathf.PI*Mathf.Rad2Deg,cirR);

                    var lefttop = worldPos + (new Vector2(-(oldSize.x/2- cirR),oldSize.y/2 )).Rotate(cangle);
                    var righttop = worldPos + (new Vector2((oldSize.x/2- cirR),oldSize.y/2 )).Rotate(cangle);
                    var leftbottom = worldPos +(new Vector2(-(oldSize.x/2- cirR),-oldSize.y/2 )).Rotate(cangle);
                    var rightbottom = worldPos +(new Vector2((oldSize.x/2- cirR),-oldSize.y/2 )).Rotate(cangle);
                    //连线
                    Handles.DrawLine(lefttop,righttop);
                    Handles.DrawLine(leftbottom,rightbottom);
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

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

            var _point = areaPivotTransform.InverseTransformPoint(center);
            var _size = new Vector2(width,heigth) /new Vector2(areaPivotTransform.lossyScale.x, areaPivotTransform.lossyScale.y);

            Rect rect = new Rect().SetSize(_size).SetCenter(_point);
            var finalSize = Vector2.zero;
            switch (direction)
            {
                
                case CapsuleDirection2D.Vertical:
                    //Y轴不做限制，X轴不能超过Y轴
                    if (rect.size.x > rect.size.y)
                    {
                        finalSize = new Vector2(rect.size.x, rect.size.x);
                    }
                    break;
                case CapsuleDirection2D.Horizontal:
                    //x轴不做限制，Y轴不能超过X轴
                    if (rect.size.x < rect.size.y)
                    {
                        finalSize = new Vector2(rect.size.y, rect.size.y);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if(finalSize != Vector2.zero)
                rect = rect.SetSize(finalSize);
            point = rect.center;
            size =rect.size;
#endif
        }

    }
}