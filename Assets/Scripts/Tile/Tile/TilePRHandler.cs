using ilsFramework;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 用来实现TileMap上的渲染与碰撞处理
/// </summary>
public class TilePRHandler : TileBase
{
    public Sprite[] tileSprites;
    
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        if (TileManager.Instance.TryGetTile(new Vector2Int(position.x,position.y),out var tile))
        {
            tile.GetRenderingData(out var renderingSprite, out var renderingColor);
            tileData.colliderType = tile.GetColliderType();
            tileData.sprite =renderingSprite;
            tileData.transform = Matrix4x4.identity;
            tileData.color =renderingColor;
            tileData.flags = TileFlags.None;
        }
        base.GetTileData(position, tilemap, ref tileData);
    }

    public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
    {
        return base.GetTileAnimationData(position, tilemap, ref tileAnimationData);
    }
}
