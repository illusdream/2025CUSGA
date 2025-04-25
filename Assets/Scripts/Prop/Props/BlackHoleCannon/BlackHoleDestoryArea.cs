using System.Collections.Generic;
using ilsFramework;
using UnityEngine;

namespace Props
{
    public class BlackHoleDestoryArea : EntityComponent,IAreaEffectProcessEntity
    {
        public override string TargetUsage => "BlackHoleDestoryArea";

        public void ProcessEntity(HashSet<EntityHandler> findEntity)
        {
            foreach (var entityHandler in findEntity)
            {
                if (entityHandler.HasEntityTag(EEntityTags.DontEffectByBlackHole))
                {
                    continue;
                }

                if (entityHandler.EntityType == EEntityType.Character.ToString())
                {
                    //播放被吸入黑洞的动画
                    if (!entityHandler.TryGetComponet(EntityComponetUsage.Buff, out BaseBuffContainer buffContainer)) continue;
                    buffContainer.AddBuff(EBuffType.InBlackHoleBuff);
                }
                else
                {
                    Destroy(entityHandler.gameObject);
                }
            }
        }
    }
}