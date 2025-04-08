using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AreaInfos.Shapes
{
    [Serializable]
    public class BoxShape : AreaShape
    {
        public Vector2 point;
        public Vector2 size;
        public float angle;

        public BoxShape(Vector2 point, Vector2 size, float angle)
        {
            this.point = point;
            this.size = size;
            this.angle = angle;
        }
#if UNITY_EDITOR
        public override void OnSceneGUI(Transform areaPivotTransform,Object clip)
        {
            
        }
#endif

    }
}