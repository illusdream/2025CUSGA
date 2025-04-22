using UnityEngine;

public class CommenTileHandler : BaseTileHandler
{
    public float DamageToTile;

    public int currentHasTile;
    
    
    public override void ApplyDamageToTile(Vector2Int targetPosition, float deltaTime)
    {
        TileManager.Instance.ApplyDamageToTile(targetPosition,DamageInfo.BuildDamageInfo(DamageToTile * deltaTime,ID),out var beHittedInfo);
        if (beHittedInfo.IsKilledEntity)
        {
            currentHasTile++;
        }
    }

    public override void TryPlaceTile(Vector2Int targetPosition)
    {

    }
}