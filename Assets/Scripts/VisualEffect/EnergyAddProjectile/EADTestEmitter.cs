using Sirenix.OdinInspector;
using TMPro.EditorUtilities;
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
            var vel = Vector2.left.Rotate((0, math.PI * 2).RandomRange()) * (speedReange.x,speedReange.y).RandomRange();
            if (CharacterManager.Instance.TryGetPlayerController(id,out PlayerController controller))
            {
              var  instance = GameObject.Instantiate(prefab,transform.position,Quaternion.identity);
              if (instance.TryGetComponent<EnergyAddProjectileController>(out var result) && instance.TryGetComponent<Rigidbody2D>(out var rigidbody))
              {
                  result.Initialize(controller.transform,controller);
                  rigidbody.velocity = vel;
              }
            }
        }
    }
}