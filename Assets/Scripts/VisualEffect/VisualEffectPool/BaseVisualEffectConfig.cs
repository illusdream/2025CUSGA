using Sirenix.OdinInspector;
using UnityEngine;
using Utils.EditorUtils;

[InlineEditor(InlineEditorObjectFieldModes.Hidden)]
public class BaseVisualEffectConfig : ScriptableObject
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
}