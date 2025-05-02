using System;
using ilsFramework;
using UnityEngine;

public class EnergyAddProjectileController : MonoBehaviour
{
        private static readonly int Color1 = Shader.PropertyToID("_Color");
        public Transform targetTransform;


        public Vector2 startPosition;

        public float cDistance;

        public float MaxDistance;

        public float Speed;

        public float slerpValue;

        public Rigidbody2D Rigidbody2D;
        
        private float oldDistance;
        
        private float oldDeltaDistance;
        

        public float deltaIncrease;

        public float maxDeltaValue;

        public float minEffectDistance;
        
        public Gradient Player1Color;
        
        public Gradient Player2Color;
        
        public TrailRenderer trailRenderer;
        
        public SpriteRenderer spriteRenderer;

        public float rotAddPerSecond;
        
        MaterialPropertyBlock materialPropertyBlock;

        private float cRot;
        public void Start()
        {
                startPosition = targetTransform.position;
        }

        public void Initialize(Transform targetTransform,PlayerController controller)
        {
                materialPropertyBlock = new MaterialPropertyBlock();
                this.targetTransform = targetTransform;
                MaxDistance = Vector2.Distance(startPosition, transform.position);
                Gradient gradient = new Gradient();
                gradient.SetKeys(
                        new []{new GradientColorKey(controller.PlayerColor,0),new GradientColorKey(new Color(255,66,230),0)},new []{new GradientAlphaKey(1,0),new (0,1)});
                trailRenderer.colorGradient = gradient;
                spriteRenderer.GetPropertyBlock(materialPropertyBlock);
                materialPropertyBlock.SetColor(Color1,controller.PlayerColor);
                spriteRenderer.SetPropertyBlock(materialPropertyBlock);
        }

        public void Update()
        {
                cRot += Time.deltaTime * rotAddPerSecond;
                transform.rotation = Quaternion.Euler(0, 0, cRot);
        }

        public void FixedUpdate()
        {
                var cDistance = Vector3.Distance(transform.position, targetTransform.transform.position);
            
                var deltaDistance = cDistance - oldDistance;
            
                var d = deltaDistance - oldDeltaDistance;
            
                d = Mathf.Clamp(d, 0, maxDeltaValue);
                d /= maxDeltaValue;
                d = 1 - d;

                
                var dir = (targetTransform.position - transform.position).normalized;
            
                var _slerpValue = Mathf.Max((1 - cDistance / MaxDistance),0.3f) * slerpValue+ d * deltaIncrease;

                var preVelocity = Vector3.Slerp(Rigidbody2D.velocity, dir.normalized*Speed, _slerpValue);
                
                Rigidbody2D.velocity = preVelocity;
                
                oldDistance = cDistance;
                oldDeltaDistance = deltaDistance;

                if (Vector2.Distance(transform.position,targetTransform.position)<=minEffectDistance)
                {
                        AddEnergy();
                }

        }

        private void AddEnergy()
        {
                Destroy(gameObject);
        }
}