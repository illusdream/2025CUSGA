using System;
using System.Collections.Generic;
using System.Linq;
using AreaInfos.Shapes;
using ilsFramework;
using Props;
using Sirenix.OdinInspector;
using UnityEngine;

public class EntityManager : ManagerSingleton<EntityManager>,IManager,IAssemblyForeach
{
    [ShowInInspector]
    private Dictionary<string, EntityCollection> entityCollections;

    private EntityManagerConfig _managerConfig;
    [ShowInInspector]
    private Dictionary<EEntityType,EntityTypeInfo> entityTypeInfos;

    private Dictionary<GameObject, SpawnSource> spwanSourcesNeedAdd;
    
    bool isOnDestory = false;

    private HashSet<EntityHandler> checkOverlapBuffer;
    public void Init()
    {
        entityCollections = new Dictionary<string, EntityCollection>();
        
        _managerConfig = Config.GetConfig<EntityManagerConfig>();
        entityTypeInfos = _managerConfig.GetEntityTypesDictionary();
        
        spwanSourcesNeedAdd = new Dictionary<GameObject, SpawnSource>();
        checkOverlapBuffer = new HashSet<EntityHandler>();
    }
    
    public void ForeachCurrentAssembly(Type[] types)
    {
        HashSet<string> noCollectionTypes = _managerConfig.EntityTypes.Select((info) => info.EntityTypeName).ToHashSet();
        //将没有Collection的EntityType分配一个BaseCollection
        foreach (var noCollectionType in noCollectionTypes)
        {
            EntityCollection instance = new EntityCollection();
            var type = Enum.Parse<EEntityType>(noCollectionType);
            if (entityTypeInfos.TryGetValue(type,out var info))
            {
                instance.InitEntityCollection(type,info);
                entityCollections.Add(noCollectionType, instance);
            }
        }
    }
    

    public void Update()
    {
        
    }

    public void LateUpdate()
    {
        
    }

    public void FixedUpdate()
    {
       
    }

    public void OnDestroy()
    {
        isOnDestory = true;
        foreach (var entityCollection in entityCollections.Values)
        {
            foreach (var obj in entityCollection)
            {
                if (obj is EntityHandler eh)
                {
                    GameObject.DestroyImmediate(eh.gameObject);
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        
    }

    public void OnDrawGizmosSelected()
    {
        
    }

    public EntityCollection GetEntityCollection(EEntityType entityType)
    {
        var collectionKeyStr = entityTypeInfos[entityType].EntityTypeName;
        return entityCollections[collectionKeyStr];
    }

    public bool TryGetEntityCollection(string entityType, out EntityCollection entityCollection)
    {
        return entityCollections.TryGetValue(entityType, out entityCollection);
    }
    
    public void RegisterEntity(EntityHandler handler)
    {
        if (isOnDestory)
        {
            return;
        }
        var handlerBelongType = handler.EntityType;
        if (entityCollections.TryGetValue(handlerBelongType,out var entityCollection))
        {
            entityCollection.AddEntityToCollection(handler);
        }
    }

    public void UnregisterEntity(EntityHandler handler)
    {
        if (isOnDestory)
        {
            return;
        }
        var handlerBelongType = handler.EntityType;
        if (entityCollections.TryGetValue(handlerBelongType,out var entityCollection))
        {
            entityCollection.RemoveEntityFromCollection(handler);
        }
    }

    public void GetEntityInArea(Collider2D areaCollider, EEntityType targetEntityType,ICollection<EntityHandler> result)
    {
        var instance = GetEntityCollection(targetEntityType);
        instance.GetEntityInArea(areaCollider, result);
    }

    public void GetEntityInArea(Collider2D areaCollider, List<EEntityType> targetEntityTypes, ICollection<EntityHandler> result)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            GetEntityInArea(areaCollider, targetEntityType, result);
        }
    }
    public void GetEntityInArea(Collider2D areaCollider, string targetEntityType, ICollection<EntityHandler> result)
    {
        if (TryGetEntityCollection(targetEntityType, out EntityCollection entityCollection))
        {
            entityCollection.GetEntityInArea(areaCollider, result);
        }
    }
    
    public void GetEntityInArea(Collider2D areaCollider,List<string> targetEntityTypes,ICollection<EntityHandler> result)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            GetEntityInArea(areaCollider, targetEntityType, result);
        }
    }



    public void GetEntityByRaycast(Vector2 rayOrigin,Vector2 rayVector,string targetEntityType,ICollection<EntityHandler> result)
    {
        if (TryGetEntityCollection(targetEntityType, out EntityCollection entityCollection))
        {
            entityCollection.GetEntityByRaycast(rayOrigin,rayVector, result);
        }
    }
    public void GetEntityByRaycast(Vector2 rayOrigin,Vector2 rayVector,List<string> targetEntityTypes,ICollection<EntityHandler> result)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            GetEntityByRaycast(rayOrigin, rayVector, targetEntityType, result);
        }
    }
    public void GetEntityByRaycast(Vector2 rayOrigin,Vector2 rayVector,EEntityType targetEntityType ,ICollection<EntityHandler> result)
    {
        var instance = GetEntityCollection(targetEntityType);
        instance.GetEntityByRaycast(rayOrigin,rayVector, result);
    }
    public void GetEntityByRaycast(Vector2 rayOrigin,Vector2 rayVector,List<EEntityType> targetEntityTypes,ICollection<EntityHandler> result)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            GetEntityByRaycast(rayOrigin, rayVector, targetEntityType, result);
        }
    }
    
    public void GetEntityByRaycast(Vector2 rayOrigin,Vector2 rayDirection,List<string> targetEntityTypes,ICollection<EntityHandler> result,float distance)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            if (TryGetEntityCollection(targetEntityType, out EntityCollection entityCollection))
            {
                entityCollection.GetEntityByRaycast(rayOrigin,rayDirection, result, distance);
            }
        }
    }
    //只做枚举版本的吧，我想偷懒
    public void GetEntityOverlapPoint(Vector2 point, EEntityType targetEntityType, ICollection<EntityHandler> result)
    {
        var instance = GetEntityCollection(targetEntityType);
        instance.GetEntityOverlapPoint(point, result);
    }

    public void GetEntityOverlapPoint(Vector2 point, List<EEntityType> targetEntityTypes, ICollection<EntityHandler> result)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            GetEntityOverlapPoint(point, targetEntityType, result);
        }
    }

    public void GetEntityOverlapBox(Vector2 point, Vector2 size,float angle, EEntityType targetEntityType,ICollection<EntityHandler> result)
    {
        var instance = GetEntityCollection(targetEntityType);
        instance.GetEntityOverlapBox(point, size, angle,result);
    }
    
    public void GetEntityOverlapBox(Vector2 point, Vector2 size,float angle, List<EEntityType> targetEntityTypes, ICollection<EntityHandler> result)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            GetEntityOverlapBox(point,size,angle ,targetEntityType, result);
        }
    }

    public void GetEntityOverlapCircle(Vector2 point, float radius, EEntityType targetEntityType,ICollection<EntityHandler> result)
    {
        var instance = GetEntityCollection(targetEntityType);
        instance.GetEntityOverlapCircle(point, radius, result);
    }
    
    public void GetEntityOverlapCircle(Vector2 point, float radius, List<EEntityType> targetEntityTypes,ICollection<EntityHandler> result)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            GetEntityOverlapCircle(point,radius,targetEntityType, result);
        }
    }

    public void GetEntityOverlapCapsule(Vector2 point,Vector2 size,CapsuleDirection2D direction2D,float angle,EEntityType targetEntityType, ICollection<EntityHandler> result)
    {
        var instance = GetEntityCollection(targetEntityType);
        instance.GetEntityOverlapCapsule(point,size,direction2D,angle,result);
    }
    
    public void GetEntityOverlapCapsule(Vector2 point,Vector2 size,CapsuleDirection2D direction2D,float angle,List<EEntityType> targetEntityTypes, ICollection<EntityHandler> result)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            GetEntityOverlapCapsule(point,size,direction2D,angle,targetEntityType, result);
        }
    }

    public bool ShapeIsOverlapByEntity(Transform shapeTransform,AreaShape shape, List<EEntityType> targetEntityTypes)
    {
        checkOverlapBuffer.Clear();
        GetEntityOverlapByShape(shapeTransform,shape,targetEntityTypes,checkOverlapBuffer);
        return checkOverlapBuffer.Count > 0;
    }
    
    public bool ShapeIsOverlapByEntity(Transform shapeTransform,AreaShape shape, EEntityType targetEntityType)
    {
        checkOverlapBuffer.Clear();
        GetEntityOverlapByShape(shapeTransform,shape,targetEntityType,checkOverlapBuffer);
        return checkOverlapBuffer.Count > 0;
    }

    public void GetEntityOverlapByShape(Transform shapeTransform,AreaShape shape, List<EEntityType> targetEntityTypes, ICollection<EntityHandler> result)
    {
        //查看形状
        switch (shape)
        {
            case BoxShape boxShape:
                boxShape.GetCurrentData(shapeTransform,out var boxPoint,out var boxSize,out var boxAngle);
                GetEntityOverlapBox(boxPoint,boxSize,boxAngle,targetEntityTypes,result);
                break;
            case CapsuleShape capsuleShape:
                capsuleShape.GetCurrentData(shapeTransform,out var capPoint,out var capSize,out var capsuleDirection2D,out var capAngle);
                GetEntityOverlapCapsule(capPoint,capSize,capsuleDirection2D,capAngle,targetEntityTypes,result);
                break;
            case CircleShape circleShape:
                circleShape.GetCurrentData(shapeTransform,out var circlePoint,out var circleSizeRadius);
                GetEntityOverlapCircle(circlePoint,circleSizeRadius,targetEntityTypes,result);
                break;
            case PointShape pointShape:
                pointShape.GetCurrentData(shapeTransform,out var pointPoint);
                GetEntityOverlapPoint(pointPoint,targetEntityTypes,result);
                break;
            case RayShape rayShape:
                rayShape.GetCurrentData(shapeTransform,out var rayStart,out var rayEnd);
                GetEntityByRaycast(rayStart,(rayEnd - rayStart),targetEntityTypes,result);
                break;
        }
    }
    public void GetEntityOverlapByShape(Transform shapeTransform,AreaShape shape, EEntityType targetEntityType, ICollection<EntityHandler> result)
    {
        //查看形状
        switch (shape)
        {
            case BoxShape boxShape:
                boxShape.GetCurrentData(shapeTransform,out var boxPoint,out var boxSize,out var boxAngle);
                GetEntityOverlapBox(boxPoint,boxSize,boxAngle,targetEntityType,result);
                break;
            case CapsuleShape capsuleShape:
                capsuleShape.GetCurrentData(shapeTransform,out var capPoint,out var capSize,out var capsuleDirection2D,out var capAngle);
                GetEntityOverlapCapsule(capPoint,capSize,capsuleDirection2D,capAngle,targetEntityType,result);
                break;
            case CircleShape circleShape:
                circleShape.GetCurrentData(shapeTransform,out var circlePoint,out var circleSizeRadius);
                GetEntityOverlapCircle(circlePoint,circleSizeRadius,targetEntityType,result);
                break;
            case PointShape pointShape:
                pointShape.GetCurrentData(shapeTransform,out var pointPoint);
                GetEntityOverlapPoint(pointPoint,targetEntityType,result);
                break;
            case RayShape rayShape:
                rayShape.GetCurrentData(shapeTransform,out var rayStart,out var rayEnd);
                GetEntityByRaycast(rayStart,(rayEnd - rayStart),targetEntityType,result);
                break;
        }
    }
    
    public GameObject Instantiate(GameObject prefab,SpawnSource spawnSource,Vector3 position,Quaternion rotation)
    {
        var go = GameObject.Instantiate(prefab,position,rotation);
        if (go.TryGetComponent<EntityHandler>(out EntityHandler component))
        {
            component.SpawnSource = spawnSource;
        }
        return go;
    }

    public void ClearAllEntities()
    {
        foreach (var entityCollection in entityCollections.Values)
        {
            var targets = entityCollection.ToList();
            foreach (var obj in targets)
            {
                if (obj is EntityHandler eh)
                {
                    GameObject.DestroyImmediate(eh.gameObject);
                }
            }
        }
    }

}