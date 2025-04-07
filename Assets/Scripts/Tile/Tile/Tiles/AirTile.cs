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

    }
    

}