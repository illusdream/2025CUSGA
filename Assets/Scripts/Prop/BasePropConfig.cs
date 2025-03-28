using Sirenix.OdinInspector;
using UnityEngine;
using Utils.EditorUtils;

/// <summary>
/// 每个道具对应的配置
/// </summary>
[InlineEditor(InlineEditorObjectFieldModes.Hidden)]
public class BasePropConfig : ScriptableObject
{
    [Title("$GetTileTypeTile",titleAlignment:TitleAlignments.Centered)]
    [HideLabel]
    [PropertyOrder(int.MinValue)]
    [ShowInInspector]
    private TopTitle TopTitle;

    private string GetTileTypeTile()
    {
        return TargetType;
    }
    [HideInInspector]
    public string TargetType;
    
    public Sprite PropSprite;
    
    public float BasePropUseColdDown = 1;
    
    

}