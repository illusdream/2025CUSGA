using System;

namespace Tiles
{
    /// <summary>
    /// 普通中立方块 这个名字有点怪
    /// </summary>
    public class CommonTile : BaseTile
    {
        public override Type TilePropertyType => typeof(CommonTileProperty);
    }


    public class CommonTileProperty : BaseTileProperty
    {

    }
}