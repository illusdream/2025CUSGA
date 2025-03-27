using System;
using ilsFramework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 方块的基类，运行时实例
/// </summary>
public abstract class BaseTile : IHitable
{
    public Vector2Int Position;
    
    public float Health;
    
    public float MaxHealth;

    public int BaseMaxHealth;
    public float HealthPercent => Health / MaxHealth;

    /// <summary>
    /// 这个Tile隶属于哪个玩家或者系统
    /// </summary>
    public EntityID TileBelongToID;

    public bool CanBeDestroyed;

    public bool CanBeMerged;

    public int BaseMergeScore;

    public int TileID;
    
    public bool IsDestroyed;

    /// <summary>
    /// 最后一个击中Tile的Player的ID
    /// </summary>
    public EntityID TileLastestBeHitByID;
    
    /// <summary>
    /// 需要的PropertyType，正式初始化时会将对应类型的tileProperty传入Initialize
    /// </summary>
    public abstract Type TilePropertyType { get; }

    public BaseTileProperty TileProperty { get; set; }
    
    public Sprite CurrentRenderSprite;

    public Color CurrentRenderColor;
    
    public Tile.ColliderType CurrentColliderType;


    
    protected BaseTile()
    {
        
    }

    public virtual void Initialize(BaseTileProperty tileProperty)
    {
        BasePropertyInitialize(tileProperty);
    }

    public void BasePropertyInitialize(BaseTileProperty tileProperty)
    {
        SetupHealthData(tileProperty.BaseMaxHealth,out MaxHealth,out Health);
        
        CanBeDestroyed = tileProperty.CanBeDestroyed;

        CanBeMerged = tileProperty.CanBeMerged;

        BaseMergeScore = tileProperty.BaseMergeScore;
        
        SetupBaseRenderAndColliderData();
    }

    public virtual void SetupHealthData(int baseMaxHealth, out float MaxHealth, out float CurrentHealth)
    {
        MaxHealth = baseMaxHealth;
        CurrentHealth = MaxHealth;
    }

    public virtual void SetupBaseRenderAndColliderData()
    {
        CurrentRenderSprite = TileProperty.DefaultSprite;
        CurrentRenderColor = TileProperty.DefaultColor;
        CurrentColliderType = TileProperty.ColliderType;
    }
    
    public virtual void Update()
    {
        
    }
    
    public virtual void Destroy()
    {
        
    }

    public virtual void ApplyDamage(DamageInfo damageInfo,int playerID)
    {

    }

    public virtual void SetTileRender(BaseTileProperty tileProperty,Tilemap renderer)
    {
        
    }

    public virtual void RemoveTileRender(BaseTileProperty tileProperty, Tilemap renderer)
    {
        
    }

    public bool CanBeHit()
    {
        return CanBeDestroyed;
    }

    public virtual void Hit(DamageInfo damageInfo, out BeHittedInfo beHittedInfo)
    {
        TileLastestBeHitByID = damageInfo.DamageFrom;
        if (!CanBeHit())
        {
            beHittedInfo = BeHittedInfo.Default;
            return;
        }
        Health -= damageInfo.baseDamage;
        var cDamage = math.min(Health,damageInfo.baseDamage);
        if (Health<=0)
        {
            IsDestroyed = true;
        }
        Health = Math.Max(Health, 0);
        beHittedInfo = new BeHittedInfo()
        {
            HasBeHittedDamage = cDamage,
            IsHitted = true,
            IsKilledEntity = IsDestroyed
        };
        Health.LogSelf();
        CalculateSpriteRenderingAfterHit();
    }

    public virtual void CalculateSpriteRenderingAfterHit()
    {
        if (!TileProperty  || TileProperty.DestoryAnimationFrames == null || TileProperty.DestoryAnimationFrames.Length == 0)
        {
            CurrentRenderSprite = TileProperty.DefaultSprite;
            return;
        }
        
        var maxIndex = TileProperty.DestoryAnimationFrames.Length - 1;
        var curIndex = Mathf.CeilToInt(maxIndex * HealthPercent);
        
        CurrentRenderSprite = TileProperty.DestoryAnimationFrames[curIndex];
        RefreshTileRenderAndCollison();
        //根据血量显示不同图片
    }

    public virtual void GetRenderingData(out Sprite renderingSprite,out Color renderingColor)
    {
        renderingColor = CurrentRenderColor;
        renderingSprite = CurrentRenderSprite;
    }

    public virtual Tile.ColliderType GetColliderType()
    {
        return CurrentColliderType;
    }

    public void RefreshTileRenderAndCollison()
    {
        TileManager.Instance.RefreshTileRenderingAndCollison(Position);
    }
}