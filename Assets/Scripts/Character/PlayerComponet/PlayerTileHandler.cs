using System;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerTileHandler : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.playerTileHandler;

        public PlayerController playerController;

        private TimerCollection timerCollection;
        private const string PlayerAttackTileTimer = "PlayerAttackTileTimer";
        
        public int PlayerDamageToTile;
        /// <summary>
        /// 玩家最大持有方块数目
        /// </summary>
        public int MaxPlayerCanHasTileCount;
        [ShowInInspector]
        public int PlayerTileCurrentHas { get;private set; }


        private bool NeedToAttackTile;

        public float MinBreakTileTime;
        
        public override void OnInitialized(EntityHandler handler)
        {
                timerCollection = new TimerCollection();
                AddEventListener(PlayerEvent.BeOrderToStartBreakTile,EEntityEventScope.Entity,Listener_BeOrderToStartBreakTile);
                AddEventListener(PlayerEvent.BeOrderToEndBreakTile,EEntityEventScope.Entity,Listener_BeOrderToEndBreakTile);
                
                AddEventListener(PlayerEvent.BeOrderToPlaceTile,EEntityEventScope.Component,Listener_BeOrderToPlaceTile);
                base.OnInitialized(handler);
        }

        public override void OnEntityDestroy(EntityHandler handler)
        {
                timerCollection.ClearAllTimers();
                RemoveEventListener(PlayerEvent.BeOrderToStartBreakTile,EEntityEventScope.Entity,Listener_BeOrderToStartBreakTile);
                RemoveEventListener(PlayerEvent.BeOrderToEndBreakTile,EEntityEventScope.Entity,Listener_BeOrderToEndBreakTile);
                
                RemoveEventListener(PlayerEvent.BeOrderToPlaceTile,EEntityEventScope.Component,Listener_BeOrderToPlaceTile);
                base.OnEntityDestroy(handler);
        }
        

        private void Listener_BeOrderToStartBreakTile(EventArgs args)
        {
                timerCollection.RemoveTimer(PlayerAttackTileTimer);
                NeedToAttackTile = true;
        }
        private void Listener_BeOrderToEndBreakTile(EventArgs args)
        {
                if (args is PlayerEvent.BeOrderToEndBreakTileEventArgs _args)
                {
                        var time = MinBreakTileTime - _args.BreakOrderContinueTime + 0.001f;
                        if (time >0)
                        {
                                timerCollection
                                        .CreateTimer(time, 1, PlayerAttackTileTimer)
                                        .SetOnFinish(_=> NeedToAttackTile = false)
                                        .Register();
                                return;
                        }
                        NeedToAttackTile = false;
                }
        }

        private void Listener_BeOrderToPlaceTile(EventArgs args)
        {
                TryPlaceTile();
        }

        public void ApplyDamageToTile()
        {
                Vector2Int pos  = TileManager.Instance.GetTilePosition(new Vector2(transform.position.x, transform.position.y));
                TileManager.Instance.ApplyDamageToTile(pos,DamageInfo.BuildDamageInfo(PlayerDamageToTile * Time.fixedDeltaTime,ID),out var beHittedInfo);
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

        public void FixedUpdate()
        {
                if (NeedToAttackTile)
                {
                        ApplyDamageToTile();
                }
        }
}