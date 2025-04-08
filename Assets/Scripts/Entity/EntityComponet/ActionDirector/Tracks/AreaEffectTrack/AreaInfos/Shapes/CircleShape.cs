using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AreaInfos.Shapes
{
    [Serializable]
    public class CircleShape : AreaShape
    {
        public Vector2 point;
        public float radius;

        public CircleShape(Vector2 point, float radius)
        {
            this.point = point;
            this.radius = radius;
        }
#if UNITY_EDITOR
        public override void OnSceneGUI(Transform areaPivotTransform,Object clip)
        {
            
        }
#endif
    }
}