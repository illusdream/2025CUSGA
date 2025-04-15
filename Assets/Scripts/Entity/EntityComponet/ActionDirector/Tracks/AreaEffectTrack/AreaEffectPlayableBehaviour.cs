using System;
using System.Collections.Generic;
using AreaInfos.Shapes;
using ilsFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class AreaEffectPlayableBehaviour : PlayableBehaviour
{
    public List<AreaInfo> AreaInfos;

    public EAreaTargetType AreaTargetType;
    
    //接下来是Entity相关的
    public List<EEntityType> TargetEntityType;
    
    public HashSet<EntityHandler> findEntities = new HashSet<EntityHandler>();
    
    public HashSet<Vector2Int> findPositions = new HashSet<Vector2Int>();

    public UnityEvent<HashSet<EntityHandler>> ApplyEffectToEntity;
    
    public UnityEvent<HashSet<Vector2Int>> ApplyEffectToTile;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
#if UNITY_EDITOR
        if(!EditorApplication.isPlaying)
            return;
#endif
        var transform = (Transform)playerData;
        //每帧实现效果
        switch (AreaTargetType)
        {
            case EAreaTargetType.Entity:
                findEntities.Clear();
                foreach (var areaInfo in AreaInfos)
                {
                    switch (areaInfo.AreaType)
                    {
                        case EAreaType.Box:
                        {
                            var box = (BoxShape)areaInfo.areaShape;
                            box.GetCurrentData(transform,out var point,out var size,out var angle);
                            EntityManager.Instance.GetEntityOverlapBox(point,size,angle,TargetEntityType,findEntities);
                        }
                            break;
                        case EAreaType.Circle:
                        {
                            var circle = (CircleShape)areaInfo.areaShape;
                            circle.GetCurrentData(transform,out var point,out var radius);
                            EntityManager.Instance.GetEntityOverlapCircle(point,radius,TargetEntityType,findEntities);
                        }
                            break;
                        case EAreaType.Capsule:
                        {
                            var capsule = (CapsuleShape)areaInfo.areaShape;
                            capsule.GetCurrentData(transform,out Vector2 point,out Vector2 size,out CapsuleDirection2D direction,out var angle);
                            EntityManager.Instance.GetEntityOverlapCapsule(point,size,direction,angle,TargetEntityType,findEntities);
                        }
                            break;
                        case EAreaType.RayCast:
                        {
                            var ray = (RayShape)areaInfo.areaShape;
                            ray.GetCurrentData(transform,out Vector2 start,out var end);
                            EntityManager.Instance.GetEntityByRaycast(start,end,TargetEntityType,findEntities);
                        }
                            break;
                        case EAreaType.Point:
                        {
                            var pointShape = (PointShape)areaInfo.areaShape;
                            pointShape.GetCurrentData(transform,out var point);
                            EntityManager.Instance.GetEntityOverlapPoint(point,TargetEntityType,findEntities);
                        }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                //根据
                break;
            case EAreaTargetType.Tile:
                findPositions.Clear();
                foreach (var areaInfo in AreaInfos)
                {
                    switch (areaInfo.AreaType)
                    {
                        case EAreaType.Box:
                        {
                            var box = (BoxShape)areaInfo.areaShape;
                            box.GetCurrentData(transform,out var point,out var size,out var angle);
                            TileManager.Instance.GetTileOverlapBox(point,size,angle,findPositions);
                        }
                            break;
                        case EAreaType.Circle:
                        {
                            var circle = (CircleShape)areaInfo.areaShape;
                            circle.GetCurrentData(transform,out var point,out var radius);
                            TileManager.Instance.GetTileOverlapCircle(point,radius,findPositions);
                        }
                            break;
                        case EAreaType.Capsule:
                        {
                            var capsule = (CapsuleShape)areaInfo.areaShape;
                            capsule.GetCurrentData(transform,out Vector2 point,out Vector2 size,out CapsuleDirection2D direction,out var angle);
                            TileManager.Instance.GetTileOverlapCapsule(point,size,direction,angle,findPositions);
                        }
                            break;
                        case EAreaType.RayCast:
                        {
                            var ray = (RayShape)areaInfo.areaShape;
                            ray.GetCurrentData(transform,out Vector2 start,out var end);
                            TileManager.Instance.GetTileByRayCast(start,end,findPositions);
                        }
                            break;
                        case EAreaType.Point:
                        {
                            var pointShape = (PointShape)areaInfo.areaShape;
                            pointShape.GetCurrentData(transform,out var point);
                            TileManager.Instance.GetTileOverlapPoint(point,findPositions);
                        }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        ApplyEffectToEntity.Invoke(findEntities);


    }
}