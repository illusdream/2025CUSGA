using System;
using ilsFramework;
using UnityEngine;

namespace Test
{
    public class TestEntityComponet : EntityComponent
    {
        EntityHandler _handler;

        public int aaa;

        public override string TargetUsage => "Test";

        public override void OnInitialized(EntityHandler handler)
        {
            _handler = handler;
            handler.AddEventListener("Test",EEntityEventScope.Entity,TestEvent);
            base.OnInitialized(handler);
        }

        public void TestEvent(EventArgs args)
        {
            111.LogSelf();
        }

        public void OnDisable()
        {
            _handler.RemoveEventListener("Test",EEntityEventScope.Entity,TestEvent);
        }

        public void OnEnable()
        {
            _handler?.AddEventListener("Test",EEntityEventScope.Entity,TestEvent);
        }
    }
}