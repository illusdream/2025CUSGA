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


    }
}