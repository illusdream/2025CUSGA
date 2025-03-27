using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Test
{
    public class TestStopPlayerRunAlway : MonoBehaviour
    {
        private List<EntityHandler> result;
        public Collider2D range;
        public EdgeCollider2D edge;
        public PolygonCollider2D collider;
        public void Update()
        {

        }

        public void FixedUpdate()
        {
            result = new List<EntityHandler>();
            EntityManager.Instance.GetEntityInArea(range,new List<EEntityType>(){EEntityType.Character},result);

            foreach (var entityHandler in result)
            {
                if (!entityHandler.TryGetComponet(EntityComponetUsage.Moveable,out PlayerMoveComponent playerMoveComponet))
                    return;
                playerMoveComponet.SetTargetVelocity(-playerMoveComponet.GetEntityVelocity());
            }
        }

        [Button]
        public void BuildRange()
        {
            var allEdgePoints = edge.points;
            if (!collider)
            {
                collider = gameObject.AddComponent<PolygonCollider2D>();
            }

            var result = new List<Vector2>(allEdgePoints);
            Vector2 centerPoint = Vector2.zero;
            foreach (var point in allEdgePoints)
            {
                centerPoint += point;
            }
            centerPoint /= (allEdgePoints.Length-1);
            result.AddRange(allEdgePoints.Select((point)=>
            {
                var dif = point - centerPoint;
                return dif *2 + centerPoint;
            }));
            collider.points =result.ToArray();
        }
    }
}