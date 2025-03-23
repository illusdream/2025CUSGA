using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils.EditorUtils;

[InlineEditor(InlineEditorObjectFieldModes.Hidden)]
[Serializable]
public  class BaseTileProperty : ScriptableObject
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
    
    [ShowInInspector]
    public  int BaseMaxHealth;
    [ShowInInspector]
    public bool CanBeDestroyed;
    [ShowInInspector]
    public bool CanBeMerged;
    [ShowInInspector]
    public int BaseMergeScore;

    [HideInInspector]
    public string TargetType;
}
