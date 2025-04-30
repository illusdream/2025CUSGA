using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Timeline;
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
    
    [DefaultValue(1)]
    public float BasePropUseColdDown;

    public TimelineAsset PlayAsset;

    public int PropCanUseCount = 1;

}