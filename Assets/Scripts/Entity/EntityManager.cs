using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

public class EntityManager : ManagerSingleton<EntityManager>,IManager,IAssemblyForeach
{

    private Dictionary<string, EntityCollection> entityCollections;

    private EntityManagerConfig _managerConfig;
    
    private Dictionary<EEntityType,EntityTypeInfo> entityTypeInfos;
    public void Init()
    {
        entityCollections = new Dictionary<string, EntityCollection>();
        
        _managerConfig = Config.GetConfig<EntityManagerConfig>();
        entityTypeInfos = _managerConfig.GetEntityTypesDictionary();
    }
    
    public void ForeachCurrentAssembly(Type[] types)
    {
        HashSet<string> noCollectionTypes = _managerConfig.EntityTypes.Select((info) => info.EntityTypeName).ToHashSet();
        //反射获取EntityCollection，并查看是否有EntityCollectionSetting，没有就将该EntityCOlleciotn的info改成Other
        foreach (var type in types)
        {
            if (type.IsAssignableFrom(typeof(EntityCollection)) && !type.IsAbstract && !type.IsDefined(typeof(EntityCollectionIgnoreAttribute),false))
            {
                var settingAttr = type.GetCustomAttributes(typeof(EntityCollectionSetting), false);
                EntityCollection instance = Activator.CreateInstance(type) as EntityCollection;
                if (settingAttr.Length > 0)
                {
                    EntityCollectionSetting setting = (EntityCollectionSetting)settingAttr[0];

                    if (entityTypeInfos.TryGetValue(setting.EntityType,out var info))
                    {
                        instance.EntityTypeInfo = info;
                        entityCollections.Add(setting.EntityType.ToString(), instance);
                        noCollectionTypes.Remove(setting.EntityType.ToString());
                    }
                }
            }
        }
        
        //将没有Collection的EntityType分配一个BaseCollection
        foreach (var noCollectionType in noCollectionTypes)
        {
            BaseEntityCollection instance = new BaseEntityCollection();
            if (entityTypeInfos.TryGetValue(Enum.Parse<EEntityType>(noCollectionType),out var info))
            {
                instance.EntityTypeInfo = info;
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
       
    }

    public void OnDrawGizmos()
    {
        
    }

    public void OnDrawGizmosSelected()
    {
        
    }

    public void RegisterEntity(EntityHandler handler)
    {
        var handlerBelongTypes = handler.EntityTypes;
        foreach (var entityType in handlerBelongTypes)
        {
            if (entityCollections.TryGetValue(entityType,out var entityCollection))
            {
                entityCollection.AddEntityToCollection(handler);
            }
        }
    }

    public void UnregisterEntity(EntityHandler handler)
    {
        var handlerBelongTypes = handler.EntityTypes;
        foreach (var entityType in handlerBelongTypes)
        {
            if (entityCollections.TryGetValue(entityType,out var entityCollection))
            {
                entityCollection.RemoveEntityFromCollection(handler);
            }
        }
    }

    public void GetEntityInArea(Collider2D areaCollider,List<string> targetEntityTypes)
    {
        
    }

}