using System;
using System.Collections.Generic;
using AreaInfos.Shapes;
using ilsFramework;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using Utils;

public class HotPursuitGOController : MonoBehaviour
{
        private HashSet<EntityHandler> _handlers = new HashSet<EntityHandler>();

        public Vector2 Size;

        private float FireAreaWidth;
        
        public float FireAreaHeight;

        public List<EEntityType> target;

        private BoxShape Shape;

        private float damage;
        
        public ParticleSystem ParticleSystem;
        [Button]
        public void Initialize(Vector2 faceDir,float perSecondDamage)
        {
       
                if (faceDir.x != 0)
                {
                        FireAreaWidth = TileManager.Instance.GetTileMapSize().height;
                }

                if (faceDir.y != 0)
                {
                        FireAreaWidth = TileManager.Instance.GetTileMapSize().width;
                }

                transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(-faceDir.y, faceDir.x) * Mathf.Rad2Deg);
                Size = new Vector2(FireAreaHeight, FireAreaWidth);
                Shape = new BoxShape(new Vector2(FireAreaHeight / 2f, 0), Size, 0);
                damage = perSecondDamage;

                var shape = ParticleSystem.shape;
                shape.scale  = new Vector3(FireAreaWidth, 0, 1);
        }
        
        public void Start()
        {
                
        }

        public void Update()
        {
                if (Shape == null)
                {
                        return;
                }
                _handlers.Clear();
                
                EntityManager.Instance.GetEntityOverlapByShape(transform,Shape,target,_handlers);
            
                ProcessEntity(_handlers);
                
        }

        public void ProcessEntity(HashSet<EntityHandler> findEntity)
        {
                foreach (var handler in findEntity)
                {
                        if (handler.TryGetComponet(EntityComponetUsage.Hitable, out BaseHitable hitable))
                        {
                                var damageInfo = DamageInfo.BuildDamageInfoBySystem(damage * Time.deltaTime);
                                hitable.Hit(damageInfo,out var beHittedInfo);
                        }
                }
        }
        
}