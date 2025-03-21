using System;
using UnityEngine;

public static class TileEvent
{
        public const string TileDestroyed = "TileDestoryed";
        
        public class TileDestroyedEventArgs : EventArgs
        {
                /// <summary>
                /// 被破坏的方块的ID
                /// </summary>
                public int DestroyedTileID;
                
                /// <summary>
                /// 破坏的位置
                /// </summary>
                public Vector2 TilePosition;

                /// <summary>
                /// 被谁破坏
                /// </summary>
                public int DestroyedByID;
        }
}