using System;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerTileHandler : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.playerTileHandler;

        public PlayerController playerController;
        
        /// <summary>
        /// 玩家最大持有方块数目
        /// </summary>
        public int MaxPlayerCanHasTileCount;
        [ShowInInspector]
        public int PlayerTileCurrentHas { get;private set; }
        
        public override void OnInitialized(EntityHandler handler)
        {
                TileManager.Instance.AddListener(TileEvent.TileDestroyed,ReciveTileDestroyed);
                AddEventListener(PlayerEvent.BeOrderToBreakTile,EEntityEventScope.Component,Listener_BeOrderToBreakTile);
                base.OnInitialized(handler);
        }

        public override void OnEntityDestroy(EntityHandler handler)
        {
                TileManager.Instance.RemoveListener(TileEvent.TileDestroyed,ReciveTileDestroyed);
                RemoveEventListener(PlayerEvent.BeOrderToBreakTile,EEntityEventScope.Component,Listener_BeOrderToBreakTile);
                base.OnEntityDestroy(handler);
        }
        
        
        //订阅方块破坏事件，来获取自己破坏了方块
        private void ReciveTileDestroyed(EventArgs args)
        {
                if (args is TileEvent.TileDestroyedEventArgs tileDestroyedEventArgs)
                {
                        111.LogSelf();
                        if (playerController.PlayerID != tileDestroyedEventArgs.DestroyedByID)
                        {
                                return;
                        }

                        PlayerTileCurrentHas++;
                }
        }

        private void Listener_BeOrderToBreakTile(EventArgs args)
        {
                ApplyDamageToTile();
        }

        public void ApplyDamageToTile()
        {
                Vector2Int pos  = TileManager.Instance.GetTilePosition(new Vector2(transform.position.x, transform.position.y));
                TileManager.Instance.ApplyDamageToTile(pos,50,playerController.PlayerID);
        }
}