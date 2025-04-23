using System;
using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class BlackHoleDragArea : EntityComponent,IAreaEffectProcessEntity
    {
        public override string TargetUsage => "BlackHoleDragArea";

        public float DragForce;

        public float BlackHoleSize = 2;
        public void ProcessEntity(HashSet<EntityHandler> findEntity)
        {
            foreach (var entityHandler in findEntity)
            {
                if (entityHandler == handler)
                {
                    continue;
                }
                var dragDir = ( transform.position-entityHandler.transform.position ).normalized *DragForce;
                dragDir *=Mathf.Max(0.3f, (1-Vector3.Distance(transform.position, entityHandler.transform.position)/BlackHoleSize));
                if (entityHandler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove move))
                {
                    move.AddForce(dragDir);
                }
            }
        }
    }
}