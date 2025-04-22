using ilsFramework;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(EntityComponent),editorForChildClasses:true)]
    public class EntityComponentEditor : OdinEditor
    {
        protected override void OnEnable()
        {
            var allField = target.GetType().GetFields();
            foreach (var field in allField)
            {
                if (field.FieldType.IsSubclassOf(typeof(AreaShape)))
                {
                    var instance = field.GetValue(target);
                    if (instance != null)
                    {
                        TrackSceneOrInsperctorManager.Instance.TrySetDrawerPivotTransfrom_Clip(instance,((EntityComponent)target).transform,target);
                        TrackSceneOrInsperctorManager.Instance.SetDrawerSceneVisbale(instance,true);
                    }
                }

            }
            base.OnEnable();
        }


        protected override void OnDisable()
        {
            var allField = target.GetType().GetFields();
            foreach (var field in allField)
            {
                if (field.FieldType.IsSubclassOf(typeof(AreaShape)))
                {
                    var instance = field.GetValue(target);
                    if (instance != null)
                    {
                        //TrackSceneOrInsperctorManager.Instance.TrySetDrawerPivotTransfrom_Clip(instance,((EntityComponent)target).transform,target);
                        TrackSceneOrInsperctorManager.Instance.SetDrawerSceneVisbale(instance,false);
                    }
                }

            }
        }
        
    }
}