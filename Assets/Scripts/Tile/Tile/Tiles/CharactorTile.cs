using System;

namespace Tiles
{
    public class CharactorTile : BaseTile
    {
        public override Type TilePropertyType => typeof(CharactorTileProperty);
    }
}