using System;
using System.Collections.Generic;
using AreaInfos.Shapes;
using Sirenix.OdinInspector;
using UnityEngine;

public interface IPropSpawnEntity
{
        public void SpawnEntity(PointShape pointShape,EntityHandler entityHandler,Transform pivotTransform);
}
[Serializable]
public class PropSpawnEntitySetter : PropInterfaceSetter,IPropHasAreaInfo
{
        public ExposedReference<Transform> pivotTransform;
        [InlineProperty]
        public PointShape pointShape = new PointShape(Vector2.zero);

        public IEnumerable<(AreaShape,ExposedReference<Transform>)> GetAllAreaShapes()
        {
                return new (AreaShape, ExposedReference<Transform>)[] { (pointShape,pivotTransform) };
        }
        
        
}