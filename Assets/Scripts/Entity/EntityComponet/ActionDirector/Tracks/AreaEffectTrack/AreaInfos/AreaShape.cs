using System;
using AreaInfos.Shapes;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]

public abstract class AreaShape
{
    public static BoxShape Box() => new BoxShape(Vector2.zero, Vector2.one, 0);
    public static CircleShape Circle() => new CircleShape(Vector2.zero,1);
    public static CapsuleShape Capsule() => new CapsuleShape(Vector2.zero, new Vector2(1,2), CapsuleDirection2D.Vertical,0);
    public static PointShape Point() => new PointShape(Vector2.zero);
    public static RayShape Ray() => new RayShape(Vector2.left, Vector2.right);
    
    public abstract void OnSceneGUI(Transform areaPivotTransform,Object clip);
}