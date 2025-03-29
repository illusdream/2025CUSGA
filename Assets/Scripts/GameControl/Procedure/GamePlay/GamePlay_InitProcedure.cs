using System;
using Cysharp.Threading.Tasks;
using ilsFramework;
using UnityEngine;
using UnityEngine.WSA;

public class GamePlay_InitProcedure : ProcedureNode
{
    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnEnter()
    {
        var levelSetting = FindLevelSetting();
        if (!levelSetting)
        {
            //default设置
        }
        
        TileManager.Instance.GenerateTiles();
        TileManager.Instance.StartFillRandomRange();
        CharacterManager.Instance.InitAllPlayers(levelSetting.Player1SpawnTransform, levelSetting.Player2SpawnTransform);
        
        base.OnEnter();
    }

    public  override void OnUpdate()
    {
        ChangeState<GamePlay_PlayingProcedure>();
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

    public async override void OnExit()
    {
        UIManager.Instance.GetUIPanel<UI_SystemFadeHandler>().FadeOut(out var fadeOutDuration);
        await UniTask.Delay(TimeSpan.FromSeconds(fadeOutDuration), DelayType.Realtime);
        CharacterManager.Instance.SetAllPlayerCanBeControlled(true);
        base.OnExit();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public LevelSetting FindLevelSetting()
    {
       return GameObject.Find(LevelSetting.LevelSettingGOName).GetComponent<LevelSetting>();
    }
}