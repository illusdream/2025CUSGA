using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPropHasAreaInfo
{
        public IEnumerable<(AreaShape,ExposedReference<Transform>)> GetAllAreaShapes();
}