using System;
using System.Collections.Generic;
using System.Diagnostics;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Test
{
    public class TestFindEntity : MonoBehaviour
    {
        public Collider2D collider2D;
        private List<EntityHandler> buffer;

        public Vector2 RaycastVector2;
        
        [Button]
        public void TestFindEntitys(EEntityType entityType)
        {
            buffer = new List<EntityHandler>();
            EntityManager.Instance.GetEntityInArea(collider2D,new List<EEntityType>(){entityType},buffer);
            foreach (var entityHandler in buffer)
            {
                entityHandler.LogSelf(entityHandler.gameObject);
            }
        }

        public void OnDrawGizmosSelected()
        {
            if (!EditorApplication.isPlaying)
            {
                return;
            }

            Vector3 trueRay = transform.rotation * RaycastVector2.Vec3_xy();
            
            bool hasTarget = false;
            buffer = new List<EntityHandler>();
            var or = new Vector2(transform.position.x, transform.position.y);
            var v2 = new Vector2(trueRay.x, trueRay.y);
            EntityManager.Instance.GetEntityByRaycast(or,v2,new List<string>() { EEntityType.Flyable.ToString() },buffer,float.NegativeInfinity);
            foreach (var entityHandler in buffer)
            {
                hasTarget = true;
                entityHandler.LogSelf(entityHandler.gameObject);
            }
            Gizmos.color = hasTarget ? Color.green : Color.red;
            
            Gizmos.DrawRay(transform.position,transform.position + trueRay);
        }
    }
}