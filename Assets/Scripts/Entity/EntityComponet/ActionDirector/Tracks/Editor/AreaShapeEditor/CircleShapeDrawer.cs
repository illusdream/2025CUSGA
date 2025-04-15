using System;
using AreaInfos.Shapes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CircleShapeDrawer : AreaShapeDrawer<CircleShape>
    {
        public override void OnDrawGUI(GUIContent label, CircleShape value)
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
            value.point =  EditorGUILayout.Vector2Field("",value.point);
            EditorGUIUtility.labelWidth = 40;
            value.radius = EditorGUILayout.FloatField("Radius",value.radius);
            EditorGUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            EditorGUIUtility.labelWidth = prev;
            this.ValueEntry.SmartValue = value;  
        }

        public override bool CanDrawTypeFilter(Type type)
        {
            return type == typeof(CircleShape);
            return base.CanDrawTypeFilter(type);
        }
    }
}