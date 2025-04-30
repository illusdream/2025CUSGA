using UnityEngine;
using UnityEngine.Timeline;

namespace Props
{
    public class SwarmMissilePropConfig : BasePropConfig
    {
        public int BaseDamage;

        public GameObject MissilePrefab;
        
        public float SpawnVelocity;

        public float LifeTime;

        public TimelineAsset AttackTimeline;
    }
}