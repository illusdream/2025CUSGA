using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntityCollection : EntityCollection
{
    public override void AddEntityToCollection(EntityHandler entity)
    {
        
    }

    public override void RemoveEntityFromCollection(EntityHandler entity)
    {
        
    }

    public override void GetEntityInArea(Collider2D areaCollider, List<EntityHandler> result)
    {
        
    }

    public override IEnumerator GetEnumerator()
    {
        throw new System.NotImplementedException();
    }
}