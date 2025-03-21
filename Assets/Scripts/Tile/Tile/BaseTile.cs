using System;
using ilsFramework;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 方块的基类，运行时实例
/// </summary>
public abstract class BaseTile
{
    public Vector2Int Position;
    
    public float Health;
    
    public float MaxHealth;

    public int BaseMaxHealth;

    /// <summary>
    /// 这个Tile隶属于哪个玩家或者系统（-1），玩家ID为自然数
    /// </summary>
    public int TileBelongToID;

    public bool CanBeDestroyed;

    public bool CanBeMerged;

    public int BaseMergeScore;

    public int TileID;
    
    public bool IsDestroyed;

    /// <summary>
    /// 最后一个击中Tile的Player的ID
    /// </summary>
    public int TileLastestBeHitByID;
    
    /// <summary>
    /// 需要的PropertyType，正式初始化时会将对应类型的tileProperty传入Initialize
    /// </summary>
    public abstract Type TilePropertyType { get; }

    protected BaseTile()
    {
        
    }

    public virtual void Initialize(BaseTileProperty tileProperty)
    {
        BasePropertyInitialize(tileProperty);
    }

    public void BasePropertyInitialize(BaseTileProperty tileProperty)
    {

        BaseMaxHealth = tileProperty.BaseMaxHealth;
        Health = BaseMaxHealth;
        CanBeDestroyed = tileProperty.CanBeDestroyed;

        CanBeMerged = tileProperty.CanBeMerged;

        BaseMergeScore = tileProperty.BaseMergeScore;
    }

    
    public virtual void Update()
    {
        
    }
    
    public virtual void Destroy()
    {
        
    }

    public virtual void ApplyDamage(float damage,int playerID)
    {
        TileLastestBeHitByID = playerID;
        Health -= damage;
        if (Health<=0)
        {
            IsDestroyed = true;
        }
        Health = Math.Max(Health, 0);
    }

    public virtual void SetTileRender(BaseTileProperty tileProperty,Tilemap renderer)
    {
        
    }

    public virtual void RemoveTileRender(BaseTileProperty tileProperty, Tilemap renderer)
    {
        
    }
}