using System;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;
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
    
    [OnValueChanged("SetDestroyFramesDefault")]
    public Sprite DefaultSprite;

    //这个要改成Texture了
    public Sprite[] DestoryAnimationFrames;
    
    public AnimationClip SpawnAnimationClip;
    
    public Color DefaultColor = Color.white;
    
    public Tile.ColliderType ColliderType =Tile.ColliderType.None;

    [HideInInspector]
    public string TargetType;
    
    private void SetDestroyFramesDefault()
    {
        if (DestoryAnimationFrames is not { Length: > 1 })
        {
            DestoryAnimationFrames = new Sprite[1] {DefaultSprite};
        }
    }
}
