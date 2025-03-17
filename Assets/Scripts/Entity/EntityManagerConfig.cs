using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

[AutoBuildOrLoadConfig("Entity/EntityManagerConfig")]
public class EntityManagerConfig : ConfigScriptObject
{
    public const string EntityTypeEnumName = "EEntityType";
    public override string ConfigName => "EntityConfig";

    public List<EntityTypeInfo> EntityTypes = new List<EntityTypeInfo>();
    
    [ShowInInspector]
    private SerializableDictionary<EEntityType, EntityTypeInfo> _entityTypes = new SerializableDictionary<EEntityType, EntityTypeInfo>();
    [Button]
    private void RefreshEntityTypesEnumCS()
    {
        ScriptGenerator generator = new ScriptGenerator();

        EnumGenerator enumGenerator = new EnumGenerator(EAccessType.Public, EntityTypeEnumName);

        foreach (var entityTypeInfo in EntityTypes)
        {
            enumGenerator.Append((entityTypeInfo.EntityTypeName,""));
        }
        
        generator.Append(enumGenerator);
        
        generator.GenerateScript(EntityTypeEnumName);

        _entityTypes = new SerializableDictionary<EEntityType, EntityTypeInfo>();
        var typeEnums = Enum.GetValues(typeof(EEntityType));
        foreach (var typeEnum in typeEnums)
        {
            var name = Enum.GetName(typeof(EEntityType),typeEnum);
            var info = EntityTypes.Find((info)=>info.EntityTypeName == name);
            if (info != null)
            {
                _entityTypes.Add((EEntityType)typeEnum,info);
            }

        }
    }

    public Dictionary<EEntityType, EntityTypeInfo> GetEntityTypesDictionary()
    {
        return _entityTypes.ToDictionary(p=>p.Key, p=>p.Value);
    }
}
