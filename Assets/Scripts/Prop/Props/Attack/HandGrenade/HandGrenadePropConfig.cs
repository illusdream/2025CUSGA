using UnityEngine;
using UnityEngine.Timeline;

namespace Props
{
    public class HandGrenadePropConfig : BasePropConfig
    {
        public float BaseDamage;

        public TimelineAsset HandGrenadeBoomTimeline;
        
        public GameObject HandGrenadeBoomerPrefab;
    }
}