using System;
using ilsFramework;
using Test;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;


    [CustomEditor(typeof(TestClip))]
    public class TestClipEditor : UnityEditor.Editor
    {
        public void OnEnable()
        {
            SceneView.duringSceneGui += SceneViewOnduringSceneGui;
        }

        private void SceneViewOnduringSceneGui(SceneView obj)
        {
            Handles.PositionHandle(Vector3.one, Quaternion.identity);
        }

        public void OnSceneGUI()
        {

        }


    }
