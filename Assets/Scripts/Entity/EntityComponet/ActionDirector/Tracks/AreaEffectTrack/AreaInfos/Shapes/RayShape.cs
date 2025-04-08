using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AreaInfos.Shapes
{
    [Serializable]
    public class RayShape : AreaShape
    {
        public Vector2 start;
        public Vector2 end;

        public RayShape(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }
#if UNITY_EDITOR
        public override void OnSceneGUI(Transform areaPivotTransform,Object clip)
        {
            
        }
#endif
    }
}