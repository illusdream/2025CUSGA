using System.Collections.Generic;
using Editor;
using ilsFramework;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class TrackSceneOrInsperctorManager : Singleton<TrackSceneOrInsperctorManager>
{
        private Dictionary<object, OdinDrawer> drawersDictionary;

        private Dictionary<object, OdinDrawer> shouldDrawSceneHandler;
        
        public TrackSceneOrInsperctorManager()
        {
                drawersDictionary = new Dictionary<object, OdinDrawer>();
                shouldDrawSceneHandler = new Dictionary<object, OdinDrawer>();
                SceneView.duringSceneGui += SceneViewOnduringSceneGui;
        }

        private void SceneViewOnduringSceneGui(SceneView obj)
        {
                foreach (var value in shouldDrawSceneHandler.Values)
                {
                        if (value is IOnSceneGUI onSceneGUI)
                        {
                                onSceneGUI.DrawSceneGUI();
                        }
                }
        }

        public void RegisterDrawer(object drawTarget,OdinDrawer drawer)
        {
                drawersDictionary.TryAdd(drawTarget, drawer);
        }

        public void SetDrawerSceneVisbale(object drawTarget, bool value)
        {
                if (value)
                {
                        if (drawersDictionary.TryGetValue(drawTarget, out var drawer))
                        {
                                shouldDrawSceneHandler.TryAdd(drawTarget,drawer);
                        }
                }
                else
                {
                        shouldDrawSceneHandler.Remove(drawTarget);
                }
        }

        public bool GetDrawerIsInSceneView(object drawTarget)
        {
                return shouldDrawSceneHandler.ContainsKey(drawTarget);
        }

        public void TrySetDrawerPivotTransfrom_Clip(object drawTarget,Transform transform, Object clip)
        {
                if (drawersDictionary.TryGetValue(drawTarget,out var drawer))
                {
                        (drawer as AreaShapeDrawer)?.SetPivotTransform(transform);
                        (drawer as AreaShapeDrawer)?.SetSaveClip(clip);
                }
        }
}