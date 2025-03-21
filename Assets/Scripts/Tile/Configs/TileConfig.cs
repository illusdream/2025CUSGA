using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;

[AutoBuildOrLoadConfig(TileManagerConfig.ConfigFilePath.TileConfigFilePath)]
public class TileConfig : ConfigScriptObject
{
    public override string ConfigName => "TileConfig";
    
    public const string TileTypeEnumName = "ETileType";
    public const string TileTypeEnumDescription = "";

    public const string TilePropertyFolderPath = "Assets/Resources/Base/Tile/TileProperities";
    
    private Dictionary<string,BaseTileProperty> TileProperties;
    
    [SerializeField]
    [ShowInInspector]
    [LabelText("TileProperties")]
    [ListDrawerSettings(ShowFoldout = false,HideAddButton = true,HideRemoveButton = true,DraggableItems = false)]
    [Searchable]
    [InlineProperty]
    [ShowInInlineEditors]
    [FoldoutGroup("TileDetailConfig")]
    private List<BaseTileProperty> DictionaryValues;
    
    [ShowInInspector]
    [SerializeField]
    private SerializableDictionary<string, int> TileIDMaps;


    private void CheckTilePropertyVaild()
    {
        if (TileProperties == null)
        {
            TileProperties = new Dictionary<string, BaseTileProperty>();
            foreach (var tileProperty in DictionaryValues)
            {
                TileProperties.Add(tileProperty.TargetType, tileProperty);
            }
        }
    }
    
    public bool TryGetTileProperty(Type type, out BaseTileProperty property)
    {
        CheckTilePropertyVaild();
        return TileProperties.TryGetValue(type.FullName, out property);
    }

    public bool TryGetTileProperty<T>(out BaseTileProperty property)
    {        
        CheckTilePropertyVaild();
        return TileProperties.TryGetValue(typeof(T).FullName, out property);
    }



#if UNITY_EDITOR
    [Button(ButtonSizes.Medium)]
    [FoldoutGroup("TileDetailConfig")]
    public void RebuildTileProperties()
    {
        DictionaryValues = new List<BaseTileProperty>();
        foreach (var type in  TypeCache.GetTypesDerivedFrom<BaseTile>())
        {
            if (type.IsAbstract)
            {
                continue;
            }
            var instance = (BaseTile)Activator.CreateInstance(type);

            CreateTilePropertyAsset(instance.TilePropertyType,type,out var tileproperty);
            DictionaryValues.Add(tileproperty);
        }
    }
    [Button(ButtonSizes.Medium)]
    public void RebuildTileIDMaps()
    {
        TileIDMaps = new SerializableDictionary<string, int>();
        foreach (var type in  TypeCache.GetTypesDerivedFrom<BaseTile>())
        {
            if (!TileIDMaps.ContainsKey(type.Name))
            {
                TileIDMaps.Add(type.Name,TileIDMaps.Count);
            }
        }
    }
    
    
    [Button(ButtonSizes.Medium)]
    public void BuildTileIDEnum()
    {
        if (TileIDMaps.Count == 0)
        {
            return;
        }
        ScriptGenerator generator = new ScriptGenerator();
        EnumGenerator enumGenerator = new EnumGenerator(EAccessType.Public, TileTypeEnumName, TileTypeEnumDescription);
        foreach (var tileIDMap in TileIDMaps)
        {
            enumGenerator.Append((tileIDMap.Key,tileIDMap.Value));
        }
        generator.Append(enumGenerator);
        
        StackTrace st  = new StackTrace(0,true);
        
        DirectoryInfo directoryInfo = new DirectoryInfo(st.GetFrame(0).GetFileName());

        string parentPath = directoryInfo.Parent.Parent.FullName;
        generator.GenerateScript("ETileType",parentPath);
        AssetDatabase.Refresh();
    }
    
    [Button(ButtonSizes.Medium)]
    public void RebuildAllSets()
    {
        RebuildTileProperties();
        RebuildTileIDMaps();
    }

    public void CreateTilePropertyAsset(Type tilePropertyType,Type tileType,out BaseTileProperty property)
    {
        var instance = ScriptableObject.CreateInstance(tilePropertyType) as BaseTileProperty;
        if (instance == null)
        {
            property = null;
            return;
        }
        instance.TargetType = tileType.FullName;
        AssetDatabase.CreateAsset(instance,TilePropertyFolderPath + $"/{tilePropertyType.Name}.asset");
        property = instance;
    }
#endif
    
    

    public void CheckTileProperty(List<Type> tileTypes)
    {
        return;
        TileProperties ??= new Dictionary<string, BaseTileProperty>();
        TileIDMaps ??= new SerializableDictionary<string, int>();
       // DictionaryValues = new List<BaseTileProperty>();
        Dictionary<string,Type> tileTotileProperties = new Dictionary<string, Type>();
        HashSet<string> needTileProperties = new HashSet<string>();
        HashSet<string> currentTileProperties = TileProperties.Select((tileProperty) => tileProperty.Key).ToHashSet();
        foreach (var tileType in tileTypes)
        {
            var instance = (BaseTile)Activator.CreateInstance(tileType);
            needTileProperties.Add(tileType.FullName);
            tileTotileProperties.Add(tileType.FullName,instance.TilePropertyType);
            if (!TileIDMaps.ContainsKey(tileType.Name))
            {
                TileIDMaps.Add(tileType.Name,TileIDMaps.Count);
            }
        }
        
        //找到没有的TileProperty
        var needAdd = needTileProperties.Except(currentTileProperties);
        //找到需要删除的TileProperty
        var needRemove = currentTileProperties.Except(needTileProperties);
        foreach (var type in needAdd)
        {
            if (tileTotileProperties.TryGetValue(type, out var tilePropertyType))
            {
                var instance = (BaseTileProperty)Activator.CreateInstance(tilePropertyType);
                TileProperties.Add(type, instance);
            }
        }

        foreach (var type in needRemove)
        {
            TileProperties.Remove(type);
        }

        foreach (var key in TileProperties.Keys)
        {
           // DictionaryValues.Add(TileProperties[key]);
        }
    }

    public bool TryGetTileID(Type type, out int tileID)
    {
        return TileIDMaps.TryGetValue(type.Name, out tileID);
    }
}