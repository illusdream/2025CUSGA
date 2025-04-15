using System.Collections.Generic;
using ilsFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class DamageTilePlayableBehaviour : PlayableBehaviour
{
        HashSet<Vector2Int> targetPositions = new HashSet<Vector2Int>();
        
        public List<AreaInfo> areaInfos;
        
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
#if UNITY_EDITOR
                if(!EditorApplication.isPlaying)
                        return;
#endif
                var tileHandler = (BaseTileHandler)playerData;
                var transform = tileHandler.transform;
                
                targetPositions.Clear();

                foreach (var areaInfo in areaInfos)
                {
                        areaInfo.FindTargetInTile(transform, targetPositions);
                }

                foreach (var vector2Int in targetPositions)
                {
                        tileHandler.ApplyDamageToTile(vector2Int,Time.fixedUnscaledDeltaTime);
                }
                base.ProcessFrame(playable, info, playerData);
        }
}