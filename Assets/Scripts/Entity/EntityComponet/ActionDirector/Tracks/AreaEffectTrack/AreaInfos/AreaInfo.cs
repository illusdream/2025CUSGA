using System;
using System.Collections.Generic;
using AreaInfos.Shapes;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

[Serializable]
public class AreaInfo
{
        [FormerlySerializedAs("EAreaType")] [OnValueChanged("OnAreaTypeChanged")]
        public EAreaType AreaType;
        [SerializeField]
        [SerializeReference]
        [InlineProperty(LabelWidth = 0)]
        [HideLabel]
        [HideReferenceObjectPicker]
        public AreaShape areaShape = AreaShape.Box();

        private void OnAreaTypeChanged()
        {
                switch (AreaType)
                {
                        case EAreaType.Box:
                                areaShape = AreaShape.Box();
                                break;
                        case EAreaType.Circle:
                                areaShape = AreaShape.Circle();
                                break;
                        case EAreaType.Capsule:
                                areaShape = AreaShape.Capsule();
                                break;
                        case EAreaType.RayCast:
                                areaShape = AreaShape.Ray();
                                break;
                        case EAreaType.Point:
                                areaShape = AreaShape.Point();
                                break;
                        default:
                                throw new ArgumentOutOfRangeException();
                }
        }

        public void FindTargetInEntity(Transform transform, List<EEntityType> targetEntityTypes, ICollection<EntityHandler> result)
        {
            switch (AreaType)
            {
                case EAreaType.Box:
                {
                    var box = (BoxShape)areaShape;
                    box.GetCurrentData(transform, out var point, out var size, out var angle);
                    EntityManager.Instance.GetEntityOverlapBox(point, size, angle, targetEntityTypes, result);
                }
                    break;
                case EAreaType.Circle:
                {
                    var circle = (CircleShape)areaShape;
                    circle.GetCurrentData(transform, out var point, out var radius);
                    EntityManager.Instance.GetEntityOverlapCircle(point, radius, targetEntityTypes, result);
                }
                    break;
                case EAreaType.Capsule:
                {
                    var capsule = (CapsuleShape)areaShape;
                    capsule.GetCurrentData(transform, out Vector2 point, out Vector2 size, out CapsuleDirection2D direction, out var angle);
                    EntityManager.Instance.GetEntityOverlapCapsule(point, size, direction, angle, targetEntityTypes, result);
                }
                    break;
                case EAreaType.RayCast:
                {
                    var ray = (RayShape)areaShape;
                    ray.GetCurrentData(transform, out Vector2 start, out var end);
                    EntityManager.Instance.GetEntityByRaycast(start, (end - start), targetEntityTypes, result);
                }
                    break;
                case EAreaType.Point:
                {
                    var pointShape = (PointShape)areaShape;
                    pointShape.GetCurrentData(transform, out var point);
                    EntityManager.Instance.GetEntityOverlapPoint(point, targetEntityTypes, result);
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void FindTargetInTile(Transform transform, ICollection<Vector2Int> result)
        {
              switch (AreaType)
                    {
                        case EAreaType.Box:
                        {
                            var box = (BoxShape)areaShape;
                            box.GetCurrentData(transform,out var point,out var size,out var angle);
                            TileManager.Instance.GetTileOverlapBox(point,size,angle,result);
                        }
                            break;
                        case EAreaType.Circle:
                        {
                            var circle = (CircleShape)areaShape;
                            circle.GetCurrentData(transform,out var point,out var radius);
                            TileManager.Instance.GetTileOverlapCircle(point,radius,result);
                        }
                            break;
                        case EAreaType.Capsule:
                        {
                            var capsule = (CapsuleShape)areaShape;
                            capsule.GetCurrentData(transform,out Vector2 point,out Vector2 size,out CapsuleDirection2D direction,out var angle);
                            TileManager.Instance.GetTileOverlapCapsule(point,size,direction,angle,result);
                        }
                            break;
                        case EAreaType.RayCast:
                        {
                            var ray = (RayShape)areaShape;
                            ray.GetCurrentData(transform,out Vector2 start,out var end);
                            TileManager.Instance.GetTileByRayCast(start,end,result);
                        }
                            break;
                        case EAreaType.Point:
                        {
                            var pointShape = (PointShape)areaShape;
                            pointShape.GetCurrentData(transform,out var point);
                            TileManager.Instance.GetTileOverlapPoint(point,result);
                        }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
        }

}