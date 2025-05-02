using System;
using System.Collections.Generic;
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
        }
        
        public const string TileBreakedByPlayer = "TileBreakedByPlayer";
        
        public class TileBreakedByPlayerEventArgs : EventArgs
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
                public EntityID DestroyedByID;

                public TileBreakedByPlayerEventArgs(int destroyedTileID, Vector2 tilePosition, EntityID destroyedByID)
                {
                        DestroyedTileID = destroyedTileID;
                        TilePosition = tilePosition;
                        DestroyedByID = destroyedByID;
                }
        }
        
        
        public const string TileMerged = "TileMerged";
        
        public class TileMergeEventArgs : EventArgs
        {
                public Dictionary<EntityID, float> scoreCollection;
                
                public Vector2 MergeStartTilePosition;
                
                public bool IsRowMerge;
                
                public bool IsColumnMerge;

                public int MergeTileCount;
        }
        
        /// <summary>
        /// 放置物块时触发
        /// </summary>
        public const string TilePlaced = "TilePlaced";
        
        public class TilePlacedEventArgs : EventArgs
        {
                public Vector2Int TilePosition;

                public int PlacedTileID;
                
                /// <summary>
                /// 如果是<see cref="EntityID.Empty"/>说明由系统生成
                /// </summary>
                public EntityID PlacedByID;

                public TilePlacedEventArgs(Vector2Int tilePosition, int placedTileID, EntityID placedByID)
                {
               
                        TilePosition = tilePosition;
                        PlacedTileID = placedTileID;
                        PlacedByID = placedByID;
                }
        }
}