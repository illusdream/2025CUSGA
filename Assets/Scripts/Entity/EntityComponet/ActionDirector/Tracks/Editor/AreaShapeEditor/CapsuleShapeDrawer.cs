using System;
using AreaInfos.Shapes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CapsuleShapeDrawer : AreaShapeDrawer<CapsuleShape>
    {
        public override void OnDrawGUI(GUIContent label, CapsuleShape value)
        {
            // 绘制字段或者属性的标签
            var rect = EditorGUILayout.GetControlRect();
            if (label != null)
            {
                rect = EditorGUI.PrefixLabel(rect, label);
            }


            var prev = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 100;
            GUILayout.BeginHorizontal();
            float height = rect.height;
            EditorGUIUtility.labelWidth = 10;
            EditorGUILayout.LabelField("Point");

            if (GUILayout.Button(EditorGUIUtility.IconContent("EditCollider") , GUILayout.MinWidth(height),GUILayout.ExpandWidth(false)))
            {
                DrawAreaShapeInSceneGUI ^= true;
                TrackSceneOrInsperctorManager.Instance.SetDrawerSceneVisbale(value,DrawAreaShapeInSceneGUI);
                SceneView.RepaintAll();
            }
            EditorGUILayout.BeginVertical();
            EditorGUIUtility.labelWidth = 0;
            value.point =  EditorGUILayout.Vector2Field("",value.point);
            value.size = EditorGUILayout.Vector2Field("",value.size);
            value.direction = (CapsuleDirection2D)EditorGUILayout.EnumPopup("",value.direction);
            value.angle =  EditorGUILayout.FloatField("Angle",value.angle* Mathf.Rad2Deg) * Mathf.Deg2Rad;
            EditorGUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            EditorGUIUtility.labelWidth = prev;
            this.ValueEntry.SmartValue = value;  
        }

        public override bool CanDrawTypeFilter(Type type)
        {
            return type == typeof(CapsuleShape);
            return base.CanDrawTypeFilter(type);
        }
    }
}