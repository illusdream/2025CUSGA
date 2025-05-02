using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Editor
{
    public class VisualEffectCodeDominHandler
    {
        [DidReloadScripts]
        private static void HandlePropConfigs()
        {
            var managerConfig= Config.GetConfigInEditor<BuffConfig>();

            if (managerConfig.AutoBuildOrUpdateSingleBuffConfigs)
            {

                List<Type> allTileTypes = TypeCache.GetTypesDerivedFrom<BaseVisualEffectPool>().ToList();
                
                managerConfig.CheckBuffProperty(allTileTypes);
            }
            
        }
    }
}