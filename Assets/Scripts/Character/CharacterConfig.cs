using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

[AutoBuildOrLoadConfig("CharacterConfig")]
public class CharacterConfig : ConfigScriptObject
{
    public override string ConfigName => "Character";
    
    public AssetReference<GameObject> characterPrefab;
    
    public GameObject characterPrefabClone;

    [LabelText("可活动范围")]
    public RectInt PlayerCanPlayRange;
    [LabelText("遇到边界后的反弹倍率")]
    public float PlayerRangeEdgeBounciness;
    [LabelText("最小可触发反弹的速度")]
    public float MinCanBounceSpeed;
    
    public Color Player1Color = Color.white;
    public Color Player2Color = Color.white;
}