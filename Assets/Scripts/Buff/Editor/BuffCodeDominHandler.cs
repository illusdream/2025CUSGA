using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Editor
{
    public class BuffCodeDominHandler
    {
        [DidReloadScripts]
        private static void HandlePropConfigs()
        {
            var managerConfig= Config.GetConfigInEditor<BuffConfig>();

            if (managerConfig.AutoBuildOrUpdateSingleBuffConfigs)
            {

                List<Type> allTileTypes = TypeCache.GetTypesDerivedFrom<BaseBuff>().ToList();
                
                managerConfig.CheckBuffProperty(allTileTypes);
            }
            
        }
    }
}