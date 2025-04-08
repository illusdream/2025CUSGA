using System;
using AreaInfos.Shapes;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]

public abstract class AreaShape
{
    public static BoxShape Box() => new BoxShape(Vector2.zero, Vector2.zero, 0);
    public static CircleShape Circle() => new CircleShape(Vector2.zero,0);
    public static CapsuleShape Capsule() => new CapsuleShape(Vector2.zero, Vector2.zero, CapsuleDirection2D.Vertical,0);
    public static PointShape Point() => new PointShape(Vector2.zero);
    public static RayShape Ray() => new RayShape(Vector2.zero, Vector2.zero);
    
    public abstract void OnSceneGUI(Transform areaPivotTransform,Object clip);
}