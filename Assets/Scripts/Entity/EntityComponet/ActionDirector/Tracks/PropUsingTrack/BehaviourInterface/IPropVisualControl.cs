using System;
using Sirenix.OdinInspector;
using UnityEngine;

public interface IPropVisualControl
{
        public void OnStartVisualModifier(Transform visualTransform);
        public void ProcessVisualModifier(Transform visualTransform,double clipDuration,double clipCurrentTime);
        
        public void OnEndVisualModifier(Transform visualTransform);
}
[Serializable]
public class PropVisualControlSetter : PropInterfaceSetter
{
        public ExposedReference<Transform> visualTransform;
}