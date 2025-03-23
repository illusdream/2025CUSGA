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
                AddEventListener(PlayerEvent.BeOrderToBreakTile,EEntityEventScope.Component,Listener_BeOrderToBreakTile);
                AddEventListener(PlayerEvent.BeOrderToPlaceTile,EEntityEventScope.Component,Listener_BeOrderToPlaceTile);
                base.OnInitialized(handler);
        }

        public override void OnEntityDestroy(EntityHandler handler)
        {
                RemoveEventListener(PlayerEvent.BeOrderToBreakTile,EEntityEventScope.Component,Listener_BeOrderToBreakTile);
                RemoveEventListener(PlayerEvent.BeOrderToPlaceTile,EEntityEventScope.Component,Listener_BeOrderToPlaceTile);
                base.OnEntityDestroy(handler);
        }
        

        private void Listener_BeOrderToBreakTile(EventArgs args)
        {
                ApplyDamageToTile();
        }

        private void Listener_BeOrderToPlaceTile(EventArgs args)
        {
                TryPlaceTile();
        }

        public void ApplyDamageToTile()
        {
                Vector2Int pos  = TileManager.Instance.GetTilePosition(new Vector2(transform.position.x, transform.position.y));
                TileManager.Instance.ApplyDamageToTile(pos,DamageInfo.BuildDamageInfo(50,ID),out var beHittedInfo);
                if (beHittedInfo.IsKilledEntity)
                {
                        PlayerTileCurrentHas++;
                }
        }

        public void TryPlaceTile()
        {
                if (CheckCanPlaceTile())
                {
                        Vector2Int pos  = TileManager.Instance.GetTilePosition(new Vector2(transform.position.x, transform.position.y));
                        TileManager.Instance.TryPlaceTile(typeof(Tiles.CharactorTile),pos,ID);
                        PlayerTileCurrentHas--;
                        PlayerTileCurrentHas = Mathf.Clamp(PlayerTileCurrentHas, 0, MaxPlayerCanHasTileCount);
                }
        }

        public bool CheckCanPlaceTile()
        {
                if (PlayerTileCurrentHas > 0)
                {
                        return true;
                }

                return false;
        }
}