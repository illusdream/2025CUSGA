using System;
using System.Collections.Generic;
using DefaultNamespace;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class PropUsingClip : PlayableAsset,ITimelineClipAsset
{
    private PropUsingPlayableBehaviour template = new PropUsingPlayableBehaviour();

    private BaseProp prop;
    private EntityHandler handler;
    
    [OnValueChanged("OnInterfaceValueChanged")]
    public EPropInterfaceType InterfaceType;
    [SerializeReference]
    [InlineProperty]
    [HideLabel]
    [HideReferenceObjectPicker]
    [HideIf("InterfaceType",EPropInterfaceType.None)]
    public PropInterfaceSetter Setter;


    public override double duration => 1;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        
        var playable = ScriptPlayable<PropUsingPlayableBehaviour>.Create(graph,template);
        PropUsingPlayableBehaviour behaviour = playable.GetBehaviour();
        behaviour.prop = prop;
        behaviour.handler = handler;
        behaviour.Setter = Setter;
        behaviour.interfaceType = InterfaceType;
        return playable;
    }

    public ClipCaps clipCaps => ClipCaps.None;

    public void SetClipProperty(BaseProp prop,EntityHandler handler)
    {
        this.prop = prop;
        this.handler = handler;
    }

    private void OnInterfaceValueChanged()
    {
        switch (InterfaceType)
        {
            case EPropInterfaceType.IPropSpawnEntity:
                Setter = new PropSpawnEntitySetter();
                break;
            case EPropInterfaceType.IPropVisualControl:
                Setter = new PropVisualControlSetter();
                break;
            case EPropInterfaceType.None:
                Setter = null;
                break;
            case EPropInterfaceType.IPropApplyEffect:
                Setter = new PropApplyEffectSetter();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}