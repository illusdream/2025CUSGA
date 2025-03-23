using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    public class CharactorTile : BaseTile
    {
        public override Type TilePropertyType => typeof(CharactorTileProperty);

        public override void Initialize(BaseTileProperty tileProperty)
        {
            base.Initialize(tileProperty);
        }

        public override void SetTileRender(BaseTileProperty tileProperty, Tilemap renderer)
        {
            if (tileProperty is CharactorTileProperty property)
            {
                renderer.SetTile(new Vector3Int(Position.x,Position.y,0),property.UseRenderTile);
            }

            base.SetTileRender(tileProperty, renderer);
        }
    }
}