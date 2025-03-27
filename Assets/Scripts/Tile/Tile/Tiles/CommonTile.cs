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
        
        public override void SetTileRender(BaseTileProperty tileProperty, Tilemap renderer)
        {
            base.SetTileRender(tileProperty, renderer);
        }

        public override void Initialize(BaseTileProperty tileProperty)
        {
            base.Initialize(tileProperty);
        }

        public override void RemoveTileRender(BaseTileProperty tileProperty, Tilemap renderer)
        {           

            base.RemoveTileRender(tileProperty, renderer);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}