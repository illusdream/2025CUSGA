
    using UnityEngine;

    public abstract class BaseTileHandler : EntityComponent
    {
        public override string TargetUsage => EntityComponetUsage.TileHandler;

        public abstract void ApplyDamageToTile(Vector2Int targetPosition,float deltaTime);
        
        public abstract void TryPlaceTile(Vector2Int targetPosition);
    }
