using UnityEngine;
using UnityEngine.Timeline;

namespace Props
{
    public class RCBmobsPropConfig : BasePropConfig
    {
        public GameObject RCBmobPrefab;

        public TimelineAsset BmobAsset;
        
        public int TimeToBmob;
        
        public int DamageToEntity;

        public int DamageToTile;
    }
}