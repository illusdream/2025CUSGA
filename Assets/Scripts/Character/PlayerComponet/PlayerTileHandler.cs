using System;
using System.Collections.Generic;
using ilsFramework;
using Sirenix.OdinInspector;
using Tiles;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerTileHandler : BaseTileHandler,IAreaEffectProcessTile
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

        [ShowInInspector] public int PlayerTileCurrentHas;

        

        public TimelineAsset digAsset;
        
        public override void OnInitialized(EntityHandler handler)
        {
                base.OnInitialized(handler);
        }

        public override void OnEntityDestroy(EntityHandler handler)
        {
                base.OnEntityDestroy(handler);
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
                        if (TileManager.Instance.TryPlaceTile(typeof(Tiles.CharactorTile),pos,ID))
                        {
                                PlayerTileCurrentHas--;
                                PlayerTileCurrentHas = Mathf.Clamp(PlayerTileCurrentHas, 0, MaxPlayerCanHasTileCount);
                        }
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
        public override void ApplyDamageToTile(Vector2Int targetPosition,float deltaTime)
        {
                TileManager.Instance.ApplyDamageToTile(targetPosition,DamageInfo.BuildDamageInfo(PlayerDamageToTile * deltaTime,ID),out var beHittedInfo);
                if (beHittedInfo.IsKilledEntity)
                {
                        PlayerTileCurrentHas++;
                }
        }

        public override void TryPlaceTile(Vector2Int targetPosition)
        {
                if (CheckCanPlaceTile())
                {
                        if (TileManager.Instance.TryPlaceTile(typeof(Tiles.CharactorTile),targetPosition,ID))
                        {
                                PlayerTileCurrentHas--;
                                PlayerTileCurrentHas = Mathf.Clamp(PlayerTileCurrentHas, 0, MaxPlayerCanHasTileCount);
                        }
                }
        }
        public void FixedUpdate()
        {

        }

        public void AddPlayerHasTile(int count)
        {
                PlayerTileCurrentHas += count;
        }

        public void ProcessTile(HashSet<Vector2Int> findEntity)
        {
                foreach (var vector2Int in findEntity)
                {
                        TryPlaceTile(vector2Int);
                }
        }
}