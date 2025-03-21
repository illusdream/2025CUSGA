using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    public class AirTile : BaseTile
    {
        public override Type TilePropertyType => typeof(AirTileProperty);
        public override void Initialize(BaseTileProperty tileProperty)
        {
            base.Initialize(tileProperty);
        }

        public override void SetTileRender(BaseTileProperty tileProperty, Tilemap renderer)
        {
            renderer.SetTile(new Vector3Int(Position.x,Position.y,0),null);
            base.SetTileRender(tileProperty, renderer);
        }
    }
    

}