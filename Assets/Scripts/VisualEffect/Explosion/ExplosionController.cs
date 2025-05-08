using System;
using ilsFramework;
using UnityEngine;

public class ExplosionController : MonoBehaviour,IPoolable
{

        public ParticleSystem particles;
        
        public ParticleSystem.MainModule particlesMain;
        public void Start()
        {
                particlesMain = particles.main;
                particlesMain.stopAction = ParticleSystemStopAction.Callback;
        }

        public void OnParticleSystemStopped()
        {
                gameObject.SetActive(false);
        }

        public void OnGet()
        {
                
        }

        public void OnRecycle()
        {
                
        }

        public void OnPoolDestroy()
        {
                       
        }
}