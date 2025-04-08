using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AreaInfos.Shapes
{
    [Serializable]
    public class CapsuleShape : AreaShape
    {
        public Vector2 point;
        public Vector2 size;
        public CapsuleDirection2D direction;
        public float angle;

        public CapsuleShape(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle)
        {
            this.point = point;
            this.size = size;
            this.direction = direction;
            this.angle = angle;
        }

#if UNITY_EDITOR
        public override void OnSceneGUI(Transform areaPivotTransform,Object clip)
        {
            
        }
#endif
    }
}