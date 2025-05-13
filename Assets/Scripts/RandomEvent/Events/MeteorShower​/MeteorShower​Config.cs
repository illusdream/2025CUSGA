using ilsFramework;
using UnityEngine;
using UnityEngine.Timeline;

public class MeteorShowerConfig : BaseRandomEventConfig
{
        public float MeteorSpawnInterval;

        public float MeteorAttackTime;
        
        public float MeteorDamage;

        public TimelineAsset MeteorAttackTimeline;
        
        public GameObject MeteorPrefab;
        
        public SoundData MeteorFallSound;
}