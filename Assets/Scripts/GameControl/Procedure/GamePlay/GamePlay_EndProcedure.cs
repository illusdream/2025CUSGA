using System;
using ilsFramework;
using UnityEngine.WSA;

public class GamePlay_EndProcedure : ProcedureNode
{
    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnEnter()
    {

        base.OnEnter();
    }

    public override void OnUpdate()
    {
        //(switcher as GamePlayProcedure)?.ChangeState<StartMenuProcedure>();
        base.OnUpdate();
    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }


}