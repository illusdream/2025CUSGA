using System;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    /// <summary>
    /// 普通中立方块 这个名字有点怪
    /// </summary>
    public class CommonTile : BaseTile
    {
        public override Type TilePropertyType => typeof(CommonTileProperty);

        public override void SetTileRender(BaseTileProperty tileProperty, Tilemap renderer)
        {
            if (tileProperty is CommonTileProperty property)
            {
                renderer.SetTile(new Vector3Int(Position.x,Position.y,0),property.UseRenderTile);
            }
            base.SetTileRender(tileProperty, renderer);
        }

        public override void RemoveTileRender(BaseTileProperty tileProperty, Tilemap renderer)
        {           

            base.RemoveTileRender(tileProperty, renderer);
        }
    }
}