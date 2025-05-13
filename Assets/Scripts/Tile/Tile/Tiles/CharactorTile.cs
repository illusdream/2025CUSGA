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
                    tileHandler.SetDestroySprite(TileProperty.DestoryAnimationFrames[0]);
                }

                return;
            }
            base.OnSpawn();
        }
    }
}