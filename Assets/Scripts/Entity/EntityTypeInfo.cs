using System;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;
[Serializable]
public class EntityTypeInfo
{
        public string EntityTypeName;
        
        public LayerMask LayerMask;

        public ContactFilter2D BuildContactFilter()
        {
                return new ContactFilter2D()
                {
                        layerMask = LayerMask,
                        useLayerMask = true,
                };
        }
}