using System.ComponentModel;
using UnityEngine;

public class EnergyAddVEConfig : BaseVisualEffectConfig
{
        public GameObject EnergyAddVEPrefab;

        [DefaultValue(30)]
        public int InitialPoolSize;
        
        [DefaultValue(100)]
        public int MaxPoolSize;

        [DefaultValue(5)]
        public float SingleVeEnergyMaxCaplity;
}