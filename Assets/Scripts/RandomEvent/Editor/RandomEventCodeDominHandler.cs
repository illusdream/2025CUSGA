using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Editor
{
    public class RandomEventCodeDominHandler
    {
        [DidReloadScripts]
        private static void HandlePropConfigs()
        {
            var managerConfig= Config.GetConfigInEditor<RandomEventConfig>();

            if (managerConfig.AutoBuildOrUpdateSingleRandomEventConfigs)
            {

                List<Type> allTileTypes = TypeCache.GetTypesDerivedFrom<BaseRandomEvent>().ToList();
                
                managerConfig.CheckVisualEffectProperty(allTileTypes);
            }
            
        }
    }
}