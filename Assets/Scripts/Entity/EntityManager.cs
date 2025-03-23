using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

public class EntityManager : ManagerSingleton<EntityManager>,IManager,IAssemblyForeach
{
    [ShowInInspector]
    private Dictionary<string, EntityCollection> entityCollections;

    private EntityManagerConfig _managerConfig;
    [ShowInInspector]
    private Dictionary<EEntityType,EntityTypeInfo> entityTypeInfos;
    
    bool isOnDestory = false;
    public void Init()
    {
        entityCollections = new Dictionary<string, EntityCollection>();
        
        _managerConfig = Config.GetConfig<EntityManagerConfig>();
        entityTypeInfos = _managerConfig.GetEntityTypesDictionary();
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

    public void GetEntityInArea(Collider2D areaCollider,List<string> targetEntityTypes,List<EntityHandler> result)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            if (TryGetEntityCollection(targetEntityType, out EntityCollection entityCollection))
            {
                entityCollection.GetEntityInArea(areaCollider, result);
            }
        }
    }

    public void GetEntityInArea(Collider2D areaCollider, List<EEntityType> targetEntityTypes, List<EntityHandler> result)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            var instance = GetEntityCollection(targetEntityType);
            instance.GetEntityInArea(areaCollider, result);
        }
    }

    public void GetEntityByRaycast(Vector2 rayOrigin,Vector2 rayVector,List<string> targetEntityTypes,List<EntityHandler> result)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            if (TryGetEntityCollection(targetEntityType, out EntityCollection entityCollection))
            {
                entityCollection.GetEntityByRaycast(rayOrigin,rayVector, result);
            }
        }
    }

    public void GetEntityByRaycast(Vector2 rayOrigin,Vector2 rayVector,List<EEntityType> targetEntityTypes,List<EntityHandler> result)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            var instance = GetEntityCollection(targetEntityType);
            instance.GetEntityByRaycast(rayOrigin,rayVector, result);
        }
    }
    
    public void GetEntityByRaycast(Vector2 rayOrigin,Vector2 rayDirection,List<string> targetEntityTypes,List<EntityHandler> result,float distance)
    {
        foreach (var targetEntityType in targetEntityTypes)
        {
            if (TryGetEntityCollection(targetEntityType, out EntityCollection entityCollection))
            {
                entityCollection.GetEntityByRaycast(rayOrigin,rayDirection, result, distance);
            }
        }
    }
}