using System;
using System.Net.NetworkInformation;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Tiles
{
    /// <summary>
    /// 普通中立方块 这个名字有点怪
    /// </summary>
    public class CommonTile : BaseTile
    {
        public override Type TilePropertyType => typeof(CommonTileProperty);
        


        public override void Initialize(BaseTileProperty tileProperty)
        {
            base.Initialize(tileProperty);
        }
        
        public override void Update()
        {
            base.Update();
        }
    }
}