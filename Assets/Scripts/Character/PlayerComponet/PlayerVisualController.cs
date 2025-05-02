using System;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerVisualController : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.playerVisualHandler;

        public SpriteRenderer spriteRenderer;
        public Animator animator;
        public Transform visualTransform;
        
        [ShowInInspector]
        public float Rotation {get;set;}
        private Vector2 dir;
        public Sprite TestAnim;
        public void Update()
        {
                visualTransform.localRotation = Quaternion.Euler(0, 0, Rotation);
                spriteRenderer.flipX = false;
                spriteRenderer.flipY = dir.x > 0 ? false : true;
        }

        public void SetRotation(float angle)
        {
                Rotation = angle;
                dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle* Mathf.Deg2Rad));
        }
}