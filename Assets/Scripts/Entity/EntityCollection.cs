using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityCollection :IEnumerable
{
      /// <summary>
      /// Entity类型
      /// </summary>
      public EntityTypeInfo EntityTypeInfo;

      public abstract void AddEntityToCollection(EntityHandler entity);

      public abstract void RemoveEntityFromCollection(EntityHandler entity);
      
      public abstract void GetEntityInArea(Collider2D areaCollider,List<EntityHandler> result);
      public abstract IEnumerator GetEnumerator();
}