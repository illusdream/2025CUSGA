using System;

namespace Tiles
{
    /// <summary>
    /// 坚硬中立方块
    /// </summary>
    public class SolidTile : BaseTile
    {
        public override Type TilePropertyType => typeof(SolidTileProperty);
    }

    public class SolidTileProperty : BaseTileProperty
    {

    }
}