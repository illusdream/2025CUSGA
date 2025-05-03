using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Internal;
using Utils.EditorUtils;

[InlineEditor(InlineEditorObjectFieldModes.Hidden)]
public class BaseRandomEventConfig : ScriptableObject
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
    
    public float EventLastTime =15;
}