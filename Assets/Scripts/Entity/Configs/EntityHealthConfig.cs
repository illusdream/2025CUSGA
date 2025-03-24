using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

[AutoBuildOrLoadConfig("Entity/EntityHealthConfig")]
public class EntityHealthConfig : ConfigScriptObject
{
    public override string ConfigName =>"HealthConfig";

    [LabelText("Manager轮询/更新顺序")]
    [HideLabel] 
    [SerializeField]
    [ShowInInspector]
    [ListDrawerSettings(HideAddButton = true,HideRemoveButton = true,DraggableItems = true,ShowFoldout = true,ShowIndexLabels = false)]
    [PropertyOrder(int.MaxValue-1)]
    public List<EHealthSourceType> HealthSourcesOrder;

    [SerializeField]
    private BiMap<EHealthSourceType,int> healthSourcesOrderBiMap;
    
    public EntityHealthConfig()
    {
#if UNITY_EDITOR
        HealthSourcesOrder = Enum.GetValues(typeof(EHealthSourceType)).Cast<EHealthSourceType>().ToList();
#endif
    }

    public int GetHealthSourceIndex(EHealthSourceType healthSourceType)
    {
        CheckHealthSourcesOrderDictionary();
        return healthSourcesOrderBiMap.GetRight(healthSourceType);
    }

    public bool TryGetHealthSourceTypeByOrderIndex(int orderIndex, out EHealthSourceType healthSourceType)
    {
        CheckHealthSourcesOrderDictionary();
        return healthSourcesOrderBiMap.TryGetLeft(orderIndex, out healthSourceType);
    }

    private void CheckHealthSourcesOrderDictionary()
    {
        CheckHealthSourcesOrderList();
        if (healthSourcesOrderBiMap is null)
        {
            healthSourcesOrderBiMap = new BiMap<EHealthSourceType, int>();
            for (int i = 0; i < HealthSourcesOrder.Count; i++)
            {
                healthSourcesOrderBiMap.Add(HealthSourcesOrder[i], i);
            }
        }
    }
    [Button]
    private void CheckHealthSourcesOrderList()
    {
        HealthSourcesOrder ??= Enum.GetValues(typeof(EHealthSourceType)).Cast<EHealthSourceType>().ToList();
    }
}