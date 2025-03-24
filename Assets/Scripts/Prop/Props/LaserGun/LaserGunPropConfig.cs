using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class LaserGunPropConfig : BasePropConfig
    {
        public GameObject projectilePrefab;

        public float Damage;

        public List<EEntityType> AttackEntityType;
    }
}