using System;
using System.Collections;
using System.Collections.Generic;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

[EntityCollectionIgnore]
public class EntityCollection :IEnumerable
{
      /// <summary>
      /// Entity类型
      /// </summary>
      public EntityTypeInfo EntityTypeInfo;
      [ShowInInspector]
      private ContactFilter2D ContactFilter;

      private List<Collider2D> colliderBuffer;

      private List<RaycastHit2D> raycastHitBuffer;
      
      private HashSet<GameObject> hasFindedEntity;
      [ShowInInspector]
      public Dictionary<GameObject, EntityHandler> GameObjectToEntityHandlerMap;
      
      public event Action<EntityHandler> OnEntityAdded;
      
      public event Action<EntityHandler> OnEntityRemoved;

      public event Action<EntityHandler> OnEntityFindedByArea;
      

      public virtual void InitEntityCollection(EntityTypeInfo entityTypeInfo)
      {
            EntityTypeInfo = entityTypeInfo;
            colliderBuffer = new List<Collider2D>();
            raycastHitBuffer = new List<RaycastHit2D>();
            ContactFilter = entityTypeInfo.BuildContactFilter();
            hasFindedEntity = new HashSet<GameObject>();
            GameObjectToEntityHandlerMap = new Dictionary<GameObject, EntityHandler>();
      }

      public virtual void AddEntityToCollection(EntityHandler entity)
      {
            GameObjectToEntityHandlerMap.Add(entity.gameObject, entity);
            OnEntityAdded?.Invoke(entity);
      }

      public virtual void RemoveEntityFromCollection(EntityHandler entity)
      {
            GameObjectToEntityHandlerMap.Remove(entity.gameObject);
            OnEntityRemoved?.Invoke(entity);
      }

      public virtual bool TryGetEntity(GameObject gameObject, out EntityHandler entity)
      {
            return GameObjectToEntityHandlerMap.TryGetValue(gameObject, out entity);
      }

      public virtual void GetEntityInArea(Collider2D areaCollider, List<EntityHandler> result)
      {
            InnerGetEntityInArea(areaCollider, result);
      }

      public IEnumerator GetEnumerator()
      {
            return GameObjectToEntityHandlerMap.Values.GetEnumerator();
      }

      protected void InnerGetEntityInArea(Collider2D areaCollider, List<EntityHandler> result)
      {
            colliderBuffer.Clear();
            hasFindedEntity.Clear();
            Physics2D.OverlapCollider(areaCollider,ContactFilter, colliderBuffer);
            foreach (var collider2D in colliderBuffer)
            {
                  if (GameObjectToEntityHandlerMap.TryGetValue(collider2D.gameObject, out EntityHandler handler) && !hasFindedEntity.Contains(handler.gameObject))
                  {
                        hasFindedEntity.Add(handler.gameObject);
                        OnEntityFindedByArea?.Invoke(handler);
                        result.Add(handler);
                  }
            }
      }

      public virtual void GetEntityByRaycast(Vector2 raycastOrigin,Vector2 raycastVector, List<EntityHandler> result)
      {
            var raycastDir = raycastVector.normalized;
            var raycastDistance = raycastVector.magnitude;
            raycastHitBuffer.Clear();
            hasFindedEntity.Clear();
            Physics2D.Raycast(raycastOrigin,raycastDir,ContactFilter,raycastHitBuffer,raycastDistance);
            foreach (var variableRaycastHit2D in raycastHitBuffer)
            {
                  if (GameObjectToEntityHandlerMap.TryGetValue(variableRaycastHit2D.collider.gameObject, out EntityHandler handler) && !hasFindedEntity.Contains(handler.gameObject))
                  {
                        hasFindedEntity.Add(handler.gameObject);
                        OnEntityFindedByArea?.Invoke(handler);
                        result.Add(handler);
                  }
            }
      }
      
      public virtual void GetEntityByRaycast(Vector2 raycastOrigin,Vector2 raycastDir, List<EntityHandler> result,float distance)
      {
            raycastHitBuffer.Clear();
            hasFindedEntity.Clear();
            Physics2D.Raycast(raycastOrigin,raycastDir,ContactFilter,raycastHitBuffer,distance);
            foreach (var variableRaycastHit2D in raycastHitBuffer)
            {
                  if (GameObjectToEntityHandlerMap.TryGetValue(variableRaycastHit2D.collider.gameObject, out EntityHandler handler) && !hasFindedEntity.Contains(handler.gameObject))
                  {
                        hasFindedEntity.Add(handler.gameObject);
                        OnEntityFindedByArea?.Invoke(handler);
                        result.Add(handler);
                  }
            }
      }

}