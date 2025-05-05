using Sirenix.OdinInspector;

using Unity.Mathematics;
using UnityEngine;
using Utils;

namespace DefaultNamespace
{
    public class EADTestEmitter : MonoBehaviour
    {
        public GameObject prefab;

        public Vector2 speedReange;
        
        [Button]
        public void Test(int id)
        {
            if (VisualEffectManager.Instance.TryGetVisualEffectPool<EnergyAddVE>(out var ve))
            {
                ve.TryEmittingVE(transform.position,Vector2.one, speedReange,10,id);
            }
        }

        [Button]
        public void testRandomEvent(ERandomEventType eventType)
        {
            RandomEventManager.Instance.AddRandomEvent(eventType);
        }
    }
}