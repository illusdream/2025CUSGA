using UnityEngine;
using UnityEngine.Timeline;

namespace Props
{
    public class MortarPropConfig : BasePropConfig
    {
        public GameObject mortarAimPrefab;
        
        public float Damage;
        
        public TimelineAsset MortarExplosionTimelineAsset;
    }
}