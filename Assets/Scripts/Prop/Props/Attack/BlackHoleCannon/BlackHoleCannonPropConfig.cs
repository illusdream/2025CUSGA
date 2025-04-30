using UnityEngine;
using UnityEngine.Timeline;

namespace Props
{
    public class BlackHoleCannonPropConfig : BasePropConfig
    {
        public GameObject blackHolePrefab;

        public float blackHoleSpeed;

        public TimelineAsset blackHoleEffectTimeline;

        public float blackHoleEffectStartTime;
    }
}