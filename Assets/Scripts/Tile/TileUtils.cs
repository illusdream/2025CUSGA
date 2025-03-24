using UnityEngine;

public static class TileUtils
{
        public static bool IsAir(Vector2Int position)
        {
              return  TileManager.Instance.IsAir(position);
        }
}