using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[InlineEditor(InlineEditorObjectFieldModes.Hidden)]
[Serializable]
public  class BaseTileProperty : ScriptableObject
{
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
