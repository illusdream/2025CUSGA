using System;
using DG.Tweening;
using ilsFramework;
using UnityEngine;

namespace Props
{
    public class MortarAimController : MonoBehaviour
    {
        private static readonly int OutR = Shader.PropertyToID("_OutR");
        private static readonly int RSize = Shader.PropertyToID("_RSize");
        private static readonly int ShiziXianLength = Shader.PropertyToID("_ShiziXianLength");
        
        public PlayerInputHandler InputHandler;

        public SpriteRenderer renderer;

        public float AimMoveSpeed;
        
        public MaterialPropertyBlock materialPropertyBlock;
        
        private TimerCollection timerCollection;
        public void Initialize(PlayerInputHandler inputHandler, Color AimColor)
        {
            timerCollection = new TimerCollection();
            materialPropertyBlock = new MaterialPropertyBlock();
            InputHandler = inputHandler;
            renderer.color = AimColor;
        }

        public void Update()
        {
            var speed = InputHandler.Move.ActionValue * (AimMoveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x + speed.x, transform.position.y + speed.y, transform.position.z);
        }
        
        
        public AnimationCurve OutRCurve;
        public AnimationCurve RCurve;
        public AnimationCurve ShiziXianCurve;
        public float AnimTime;
        public void EndAim()
        {
            renderer.GetPropertyBlock(materialPropertyBlock);
            
            
            timerCollection.CreateTimer(AnimTime, 1, "EndAnim").SetOnCycling(EndAnim).SetOnFinish(OnAnimFinish).Register();


        }

        private void EndAnim(Timer timer)
        {
            materialPropertyBlock.SetFloat(OutR,OutRCurve.Evaluate(timer.Progress));
            materialPropertyBlock.SetFloat(RSize,RCurve.Evaluate(timer.Progress));
            materialPropertyBlock.SetFloat(ShiziXianLength,ShiziXianCurve.Evaluate(timer.Progress));
            renderer.SetPropertyBlock(materialPropertyBlock);
        }

        private void OnAnimFinish(Timer timer)
        {
            Destroy(gameObject);
        }
    }
}