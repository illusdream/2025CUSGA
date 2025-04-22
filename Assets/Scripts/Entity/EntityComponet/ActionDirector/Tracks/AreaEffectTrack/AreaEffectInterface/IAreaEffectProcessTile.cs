using System.Collections.Generic;
using UnityEngine;

public interface IAreaEffectProcessTile
{
        public void ProcessTile(HashSet<Vector2Int> findEntity);
}