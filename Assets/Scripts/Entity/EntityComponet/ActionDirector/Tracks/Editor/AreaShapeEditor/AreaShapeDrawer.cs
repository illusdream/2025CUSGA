using AreaInfos.Shapes;
using ilsFramework;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public abstract class AreaShapeDrawer : OdinValueDrawer<AreaShape>,IOnSceneGUI
    {
        public bool DrawAreaShapeInSceneGUI;
        
        public Transform ShapePivotTransform;

        public Object SaveClip;
                
        protected override void Initialize()
        {
            DrawAreaShapeInSceneGUI = false;
            TrackSceneOrInsperctorManager.Instance.RegisterDrawer(this.ValueEntry.SmartValue,this);
            base.Initialize();
        }
        public void SetPivotTransform(Transform transform)
        {
            ShapePivotTransform = transform;
        }

        public void SetSaveClip(Object clip)
        {
            SaveClip = clip;
        }
        public abstract void DrawSceneGUI();

    }
    public abstract class AreaShapeDrawer<TShape> : AreaShapeDrawer where TShape : AreaShape
    {

        protected override void DrawPropertyLayout(GUIContent label)
        {
            TShape value = (this.ValueEntry.SmartValue as TShape);
            if (value is null)
            {
                return;
            }
            DrawAreaShapeInSceneGUI = TrackSceneOrInsperctorManager.Instance.GetDrawerIsInSceneView(value);
            // 绘制字段或者属性的标签
            //交给后人
            OnDrawGUI(label,value);
        }
        public override void DrawSceneGUI()
        {
            AreaShape value = this.ValueEntry.SmartValue;
            if (SaveClip && ShapePivotTransform)
            {
                value.OnSceneGUI(ShapePivotTransform,SaveClip);
            }
        }
        public abstract void OnDrawGUI(GUIContent label,TShape value);


    }
    
}