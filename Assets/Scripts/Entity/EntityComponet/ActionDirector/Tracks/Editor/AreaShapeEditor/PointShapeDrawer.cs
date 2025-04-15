using System;
using AreaInfos.Shapes;
using ilsFramework;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{

    public class PointShapeDrawer : AreaShapeDrawer<PointShape>
    {
        public override void OnDrawGUI(GUIContent label, PointShape value)
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

            if (GUILayout.Button(DrawAreaShapeInSceneGUI ? EditorGUIUtility.IconContent("MoveTool on") :EditorGUIUtility.IconContent("MoveTool") , GUILayout.MinWidth(height),GUILayout.ExpandWidth(false)))
            {
                DrawAreaShapeInSceneGUI ^= true;
                TrackSceneOrInsperctorManager.Instance.SetDrawerSceneVisbale(value,DrawAreaShapeInSceneGUI);
                SceneView.RepaintAll();
            }
            EditorGUILayout.BeginVertical();
            value.point =  EditorGUILayout.Vector2Field("",value.point);
            EditorGUILayout.EndVertical();
            GUILayout.EndHorizontal();
            EditorGUIUtility.labelWidth = prev;
            this.ValueEntry.SmartValue = value;  
        }

        public override bool CanDrawTypeFilter(Type type)
        {
            return type == typeof(PointShape);
            return base.CanDrawTypeFilter(type);
        }
    }
}