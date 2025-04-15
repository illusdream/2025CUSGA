using System.Collections.Generic;
using UnityEngine.Playables;

public class AttackEntityPlayableBehaviour : PlayableBehaviour
{
    public List<AreaInfo> AreaInfo;

    public List<EEntityType> TargetEntityType;
    
    public HashSet<EntityHandler> targetEntity = new HashSet<EntityHandler>();

    public bool CanAttackSpawnerOrSpawnerOwner;
    
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var attacker = (BaseAttacker)playerData;
        var transform = attacker.transform;
        targetEntity.Clear();
        foreach (var areaInfo in AreaInfo)
        {
            areaInfo.FindTargetInEntity(transform,TargetEntityType,targetEntity);
        }

        foreach (var entityHandler in targetEntity)
        {
            if ((entityHandler.ID == attacker.ID || entityHandler.ID == attacker.handler.SpawnSource.SpawnerID) && !CanAttackSpawnerOrSpawnerOwner)
            {
                return;
            }
            attacker.Attack(entityHandler);
        }
        base.ProcessFrame(playable, info, playerData);
    }
}