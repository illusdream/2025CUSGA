using ilsFramework;
using UnityEngine;

[AutoBuildOrLoadConfig("CharacterConfig")]
public class CharacterConfig : ConfigScriptObject
{
    public override string ConfigName => "Character";
    
    public AssetReference<GameObject> characterPrefab;
    
    public GameObject characterPrefabClone;
}