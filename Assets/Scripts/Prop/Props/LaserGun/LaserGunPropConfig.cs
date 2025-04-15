using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Timeline;

namespace Props
{
    public class LaserGunPropConfig : BasePropConfig
    {
        public GameObject projectilePrefab;

        public float Damage;

        public List<EEntityType> AttackEntityType;

        public AnimationCurve lerpCurve;

        [LabelText("反推力")]
        public float ShootForce;

        public TimelineAsset LaserGameObjectTimeline;
    }
}