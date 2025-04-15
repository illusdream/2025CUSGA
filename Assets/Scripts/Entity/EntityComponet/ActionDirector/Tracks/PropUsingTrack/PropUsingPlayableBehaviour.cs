using System;
using System.Collections.Generic;
using DefaultNamespace;
using ilsFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class PropUsingPlayableBehaviour : PlayableBehaviour
{
        public BaseProp prop;
        public EntityHandler handler;

        public EPropInterfaceType interfaceType;
        public PropInterfaceSetter Setter;
        
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
                if (prop is IPropStartAnimation propStartAnimation)
                {
                        propStartAnimation.OnStartAnimation();
                }
                InterfaceOnPlayHandle(playable);
                base.OnBehaviourPlay(playable, info);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
#if UNITY_EDITOR
                if (!EditorApplication.isPlaying)
                {
                        return;
                }
#endif
                if (prop is IPropUpdate propUpdate)
                {
                        propUpdate.UpdateOnUsingProp(handler);
                }
                InterUpdateHandle(playable);
                base.ProcessFrame(playable, info, playerData);
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
                InterfaceOnStopHandle(playable);
                if (playable.GetGraph().IsPlaying()&& prop is IPropEndAnimation propEndAnimation)
                {
                        propEndAnimation.OnEndAnimation();
                }
                base.OnBehaviourPause(playable, info);
        }


        public void InterfaceOnPlayHandle(Playable playable)
        {
                switch (interfaceType)
                {
                        case EPropInterfaceType.IPropSpawnEntity:
                                break;
                        case EPropInterfaceType.IPropVisualControl:
                                if (prop is IPropVisualControl propVisual && Setter is PropVisualControlSetter setter)
                                {
                                        propVisual.OnStartVisualModifier(setter.visualTransform.Resolve(playable.GetGraph().GetResolver()));
                                }
                                break;
                        case EPropInterfaceType.None:
                                break;
                        case EPropInterfaceType.IPropApplyEffect:
                                break;
                        default:
                                throw new ArgumentOutOfRangeException();
                }
        }

        public void InterUpdateHandle(Playable playable)
        {
                switch (interfaceType)
                {
                        case EPropInterfaceType.IPropSpawnEntity:
                        {
                                if (prop is IPropSpawnEntity propSpawn && Setter is PropSpawnEntitySetter setter)
                                {
                                        propSpawn.SpawnEntity(setter.pointShape,handler,setter.pivotTransform.Resolve(playable.GetGraph().GetResolver()));
                                }
                        }
                                break;
                        case EPropInterfaceType.IPropVisualControl:
                        {
                                if (prop is IPropVisualControl propVisual && Setter is PropVisualControlSetter setter)
                                {
                                        propVisual.ProcessVisualModifier(setter.visualTransform.Resolve(playable.GetGraph().GetResolver()),playable.GetDuration(),playable.GetTime());
                                }
                        }
                                break;
                        case EPropInterfaceType.None:
                                break;
                        case EPropInterfaceType.IPropApplyEffect:
                        {
                                if (prop is IPropApplyEffect propApplyEffect && Setter is PropApplyEffectSetter setter)
                                {
                                        propApplyEffect.ApplyEffect(handler);
                                }
                        }
                                break;
                        default:
                                throw new ArgumentOutOfRangeException();
                }
        }
        
        public void InterfaceOnStopHandle(Playable playable)
        {
                switch (interfaceType)
                {
                        case EPropInterfaceType.IPropSpawnEntity:
                                break;
                        case EPropInterfaceType.IPropVisualControl:
                                if (prop is IPropVisualControl propVisual && Setter is PropVisualControlSetter setter)
                                {
                                        propVisual.OnEndVisualModifier(setter.visualTransform.Resolve(playable.GetGraph().GetResolver()));
                                }

                                break;
                        case EPropInterfaceType.None:
                                break;
                        case EPropInterfaceType.IPropApplyEffect:
                                break;
                        default:
                                throw new ArgumentOutOfRangeException();
                }
        }
        
        
}