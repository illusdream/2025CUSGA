using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using UnityEditor;
using UnityEditor.Callbacks;

public class PropCodeDominHandler
{
    [DidReloadScripts]
    private static void HandlePropConfigs()
    {
        var managerConfig= Config.GetConfigInEditor<PropConfig>();

        if (managerConfig.AutoBuildOrUpdateSinglePropConfigs)
        {
            var propConfig = Config.GetConfigInEditor<PropConfig>();

            List<Type> allTileTypes = TypeCache.GetTypesDerivedFrom<BaseProp>().ToList();
                
            propConfig.CheckTileProperty(allTileTypes);
        }
            
    }
}