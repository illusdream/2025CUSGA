using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    public class CharactorTile : BaseTile
    {
        public override Type TilePropertyType => typeof(CharactorTileProperty);

        public override void Initialize(BaseTileProperty tileProperty)
        {
            base.Initialize(tileProperty);
        }

        public override void OnSpawn()
        {
            if (TileProperty is CharactorTileProperty charactorProperty)
            {
                if (CharacterManager.Instance.IsPlayer1(TileBelongToID))
                {
                    tileHandler.PlayTileAnimation(charactorProperty.Charactor1SpawnClip);
                    tileHandler.SetDestroySprite(TileProperty.DestoryAnimationFrames[0]);
                }
                
                if (CharacterManager.Instance.IsPlayer2(TileBelongToID))
                {
                    tileHandler.PlayTileAnimation(charactorProperty.Charactor2SpawnClip);
                    tileHandler.SetDestroySprite(charactorProperty.BlueAnimationFrames[0]);
                }
                return;
            }
            base.OnSpawn();
        }

        public override void CalculateSpriteRenderingAfterHit()
        {
            if (!TileProperty  || TileProperty.DestoryAnimationFrames == null || TileProperty.DestoryAnimationFrames.Length == 0)
            {
                CurrentRenderSprite = TileProperty.DefaultSprite;
                return;
            }
        
            var maxIndex = TileProperty.DestoryAnimationFrames.Length - 1;
            var curIndex = Mathf.CeilToInt(maxIndex * (1-HealthPercent));
        
            if (CharacterManager.Instance.IsPlayer1(TileBelongToID))
            {
                CurrentRenderSprite = TileProperty.DestoryAnimationFrames[curIndex];
            }

            if (CharacterManager.Instance.IsPlayer2(TileBelongToID) && TileProperty is CharactorTileProperty charactorProperty)
            {
                CurrentRenderSprite = charactorProperty.BlueAnimationFrames[curIndex];
            }
            

        
            tileHandler.SetDestroySprite(CurrentRenderSprite);
        }
    }
}