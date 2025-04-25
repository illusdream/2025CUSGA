using System.Collections.Generic;
using UnityEngine;

public interface IAreaEffectProcess
{
        public void Process(List<AreaInfo> areas,Transform pivot,List<EEntityType> types);
}