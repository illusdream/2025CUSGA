using System;
using AreaInfos.Shapes;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

[Serializable]
public class AreaInfo
{
        [FormerlySerializedAs("EAreaType")] [OnValueChanged("OnAreaTypeChanged")]
        public EAreaType AreaType;
        [SerializeField]
        [SerializeReference]
        [InlineProperty(LabelWidth = 0)]
        [HideLabel]
        [HideReferenceObjectPicker]
        public AreaShape areaShape = AreaShape.Box();

        private void OnAreaTypeChanged()
        {
                switch (AreaType)
                {
                        case EAreaType.Box:
                                areaShape = AreaShape.Box();
                                break;
                        case EAreaType.Circle:
                                areaShape = AreaShape.Circle();
                                break;
                        case EAreaType.Capsule:
                                areaShape = AreaShape.Capsule();
                                break;
                        case EAreaType.RayCast:
                                areaShape = AreaShape.Ray();
                                break;
                        case EAreaType.Point:
                                areaShape = AreaShape.Point();
                                break;
                        default:
                                throw new ArgumentOutOfRangeException();
                }
        }
        
        

}